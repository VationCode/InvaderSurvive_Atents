//**********23.01.12 : 몬스터 받는 데미지 파티클, hp에 따른 죽는 표현 완료
//**********23.01.12 : 플레이어 추적 및 공격(모션만) 완성(공격 패턴 넣어야함(차후 보스나 다른 패턴있는 몬스터들 관리 편하게 하기 위해))
//**********23.01.11 : 몬스터 클래스 처리를 FSM으로 변경중 BT 디자인패턴으로 적용했으나 내용이 어려워 이해를 못했기에 사용은 다음에
//**********23.01.09 : MonsterInfo 확인, 플레이어의 공격 및 거리에 따른 행동 제작중
//**********Monster : 몬스터 클래스 : 몬스터에 관한 정보를 일단은 수동으로 정하는 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterT : MonoBehaviour, IDamageabel
{
   
    public GameObject player;
    public MonsterInfos monsterInfos;
    public Vector3[] wayPoints;
  
    [Tooltip("플레이어 발견 거리")]
    [SerializeField] private float discoverDis;
    [Tooltip("공격 거리 혹은 멈추는 거리")]
    public float stopDis;
    [Tooltip("원래 거리와의 이탈 허용 거리")]
    public float originPosDis;

    [SerializeField]
    private GameObject m_bloodSprayEffect;

    /*[Tooltip("원래 자리로부터 이탈할 수 있는 거리")]
    [SerializeField] private float originPosBreakAwayDis;
    [Tooltip("플레이어와 몬스터간 이탈 거리")]
    [SerializeField] private float playerPosBreakAwayDis;*/

    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public bool isApplyDamage;
    [HideInInspector] public bool isDead;

    [HideInInspector]public Vector3 originPos;
    [HideInInspector] public Quaternion originRot;
    [HideInInspector] public MonsterBaseStateT currentState;
    [HideInInspector] public Monster_PatrolState patrol; //웨이포인트들 순찰
    [HideInInspector] public Monster_DiscoverMoveState discoverMove;
    [HideInInspector] public Monster_ReturnOriginPosMoveState returnMove;
    [HideInInspector] public Monster_AttackState attack;
    [HideInInspector] public Monster_DeadState dead;

    private int playerLayer;
    private int groundLayer;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        monsterInfos.currentHP = monsterInfos.hp;

        //ResourceManager.Instance.LoadrcDamageEffect();
        //wayPoints = new Vector3[3];
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        originPos = this.transform.position;
        originRot = this.transform.rotation;
        patrol = new Monster_PatrolState();
        discoverMove = new Monster_DiscoverMoveState();
        returnMove = new Monster_ReturnOriginPosMoveState();
        attack = new Monster_AttackState();
        dead = new Monster_DeadState();
        nav.enabled = false;

        CreateWayPoint();
        //SwitchState(patrol);

        player = PlayerMovementManager.Instance.gameObject;
    }



    // Update is called once per frame
    void Update()
    {
        /*playerDis = Vector3.Distance(gameObject.transform.position, player.transform.position);
        Chase();*/
        currentState.UpdateState(this);
        Debug.Log(monsterInfos.hp);
    }

    void CreateWayPoint()
    {
        wayPoints[0] = originPos;

        if (wayPoints.Length < 2) return;
        for (int i = 1; i < wayPoints.Length; i++)
        {
            float x = transform.position.x + (Random.Range(1, 5) * 3);
            RaycastHit hit;
            float y = 0f;
            float z = transform.position.z + (Random.Range(1, 5) * 3);
            if (Physics.Raycast(new Vector3(x, 0, z), Vector3.down, out hit, Mathf.Infinity, groundLayer)) //맵의 지형에 따른 높이 구하기
            {
                y = hit.point.y;
            }
            else if (Physics.Raycast(new Vector3(x, 0, z), Vector3.up, out hit, Mathf.Infinity, groundLayer))
            {
                y = hit.point.y;
            }
            wayPoints[i] = new Vector3(x, y, z);
            
        }
    }



    public bool Discover()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= discoverDis) return true;
        return false;
    }

    public void CreateMonster()
    {

    }
    public void SwitchState(MonsterBaseStateT state) //상태 전환 함수
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public virtual bool ApplyDamage(DamageMessage damageMessage)
    {
        //if (damageMessage.damager == gameObject) return false;
        //생명력 깍기
        if (!isDead)
        {
            isApplyDamage = true;
            monsterInfos.hp -= damageMessage.damage;
            GameObject bloodEffectObj = m_bloodSprayEffect;
            //GameObject bloodEffectObj = ResourceManager.Instance.GetrcDamageEffect("BloodSprayEffect");
            ParticleSystem bloodEffect = bloodEffectObj.GetComponent<ParticleSystem>();
            bloodEffect = Instantiate(bloodEffect, damageMessage.hitPoint, Quaternion.LookRotation(damageMessage.hitNormal));
            bloodEffect.transform.SetParent(this.gameObject.transform);
            bloodEffect.transform.position = damageMessage.hitPoint;
            bloodEffect.Play();
        }
        if (monsterInfos.hp <= 0) isDead = true;
        return true;
    }
}
