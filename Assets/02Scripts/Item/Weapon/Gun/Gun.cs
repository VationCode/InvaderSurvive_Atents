//**********23.01.12 : IDamageble 컴포넌트 받아오는것 부모로 변경
//**********23.01.05 : 재장전 UI 및 탄 수에 이따른 기능 구현
//**********23.01.04 : Gun  생성후 행해질 Action들 제작중(현재 발사 및 파티클 생성)
//**********Gun : Gun 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunInfo gunInfo;
    
    public GameObject firePos;
    [SerializeField] private GameObject m_gunRightAttach;
    private RightHandTracking rightHandTracking;
    
    private const string particleNode = "03rcParticles/GunEffect/WeaponEffects/Prefabs/";
    [SerializeField] 
    private GameObject weaponParticleObj;
    private ParticleSystem particle;
    private bool particleDuplicate;
   
    private const string audioNode = "04rcAudios/ItemEffectSound/Gun/";
    private AudioClip shootAudioClip;
    private AudioClip reloadAudioClip;
    private AudioSource audioSource;

    public LayerMask excludeTarget; //조준에서 제외 레이어
    private Vector3 aimPoint;

    //private const string bulletmaterialNode = "01rcModels/Item/Weapons/_Bullet/";
    [SerializeField]
    private Material m_bulletMaterial;
    private LineRenderer bulletLineRenderer;

    //private const string skillParticleNode = "03rcParticles/SkillEffect/_Prefab/";
    //private GameObject skillEParticleObj;


    void Start()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("WeaponPos");
        transform.SetParent(parent.transform);

        rightHandTracking = GetComponent<RightHandTracking>();
        firePos = GameObject.FindGameObjectWithTag("FirePos");
        for (int i = 0; i < firePos.transform.childCount - 1; i++)
        {
            if(firePos.transform.Find(gunInfo.particleName) != null) //파티클 여부확인
            {
                particleDuplicate = true;
                break;
            }
        }
        if(!particleDuplicate)
        {
            //weaponParticleObj = Resources.Load<GameObject>(particleNode + gunInfo.particleName);

            weaponParticleObj = Instantiate(weaponParticleObj);
            weaponParticleObj.transform.position = firePos.transform.position;
            weaponParticleObj.transform.rotation = firePos.transform.rotation;
            weaponParticleObj.transform.SetParent(firePos.transform);
            particle = weaponParticleObj.GetComponent<ParticleSystem>();
        }

        shootAudioClip = Resources.Load<AudioClip>(audioNode + "Shoot/" + gunInfo.shootSoundName);
        audioSource = GetComponent<AudioSource>();

        if (excludeTarget != (excludeTarget | (1 << gameObject.layer))) //비트연산을 통해 플레이어 캐릭터의 레이어가 excludeTarget에 포함되어있지 않다면 포함되게 만듬
        {                                                               //플레이어가 자신을 쏘는 일이 없도록 미리 예외 처리
            excludeTarget |= 1 << gameObject.layer;
        }

        bulletLineRenderer = gameObject.AddComponent<LineRenderer>(); //총알 발사 궤적
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.startWidth = 0.02f;
        bulletLineRenderer.endWidth = 0.02f;
        //bulletLineRenderer.material = Resources.Load<Material>(bulletmaterialNode + gunInfo.bulletMaterrialName);
        bulletLineRenderer.material = m_bulletMaterial;

        bulletLineRenderer.enabled = false;

        gunInfo.magAmmo = gunInfo.magCapacity; //현재 탄창에 남아있는 탄의 수 최대로 초기화
    }
    void Update()
    {
        UIManager.Instance.GunAmmo(gunInfo.ammoRemain, gunInfo.magAmmo);
    }

    public void Fire()
    {
        RaycastHit hit;

        aimPoint = CameraManager.Instance.aimPos.position; //카메라가 바라보는 방향에서의 Hit 위치
        if (Physics.Linecast(firePos.transform.position, aimPoint, out hit, ~excludeTarget)) //캐릭터가 바라보는 방향에서 카메라의 정중앙 사이 장애물이 있을경우 장애물쪽 히트포인트 사용
        {
            aimPoint = hit.point;
            var target = hit.collider.GetComponentInParent<IDamageabel>(); //IDamageable메세지를 받아왔다면 데미지를 줄 수 있는 대상이라는것(부모에서 받아오는 이유는 콜라이더들이 자식으로 배치 되어 있는 모델도 있기에)
            if (target != null)
            {
                Debug.Log(hit.transform.gameObject.name);
                DamageMessage damageMessage;
                damageMessage.damager = PlayerMovementManager.Instance.playerObj;
                damageMessage.damage = gunInfo.damage;
                damageMessage.hitPoint = hit.point;
                damageMessage.hitNormal = hit.normal;

                // 상대방의 OnDamage 함수를 실행시켜서 상대방에게 데미지 주기
                target.ApplyDamage(damageMessage);
            }
        }
        var fireDirection = aimPoint - firePos.transform.position;
        StartCoroutine(ShootEffect(aimPoint));

        gunInfo.magAmmo--;
    }
    
    IEnumerator ShootEffect(Vector3 hitPosition)
    {
        particle.Play();
        audioSource.PlayOneShot(shootAudioClip);//소리를 연달아 재생하는경우 Play()보단 PlayOneShot()을 사용
        //Play()는 플레이를 하기전 오디오소스 클립에 사용할 클립을 할당해야하며
        //Play()가 실행될땐 직전까지 진행중이던 소리를 정지하고 재생하기에 총처럼 연달아 내야하는 소리에는 부자연스러움(소리가 중첩이 안된다.)

        bulletLineRenderer.SetPosition(0, firePos.transform.position); //라인랜더러 사용(인덱스, 위치값)
        bulletLineRenderer.SetPosition(1, hitPosition); //맞은 위치
        bulletLineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        //0.03초 텀 이유는 bulletLineRenderer.enabled가 활성화 된다음 바로 비활성화되서 궤적이 아예 그려지지않음

        bulletLineRenderer.enabled = false;
    }

    public void Reload()
    {
        reloadAudioClip = Resources.Load<AudioClip>(audioNode + "Reload/" + gunInfo.reloadSoundName);
        audioSource.PlayOneShot(reloadAudioClip);
        var ammoToFill = Mathf.Clamp(gunInfo.magCapacity -gunInfo.magAmmo, 0, gunInfo.ammoRemain); //채울 탄약 개수, (탄창의 최대 개수 - 현재 탄창 탄 개수, 0에서, 현재 남은 탄약 수)
        gunInfo.magAmmo += ammoToFill;
        gunInfo.ammoRemain -= ammoToFill;
    }

    private void LateUpdate()
    {
        rightHandTracking.HandTracking(m_gunRightAttach);
    }
}
