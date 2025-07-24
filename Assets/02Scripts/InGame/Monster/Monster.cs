//**********23.01.16 : 각 몬스터별로 상속받도록? 변경 중 - 허인호
//**********23.01.16 : csv파일로부터 데이터 받아오게하는 방식으로 변경 - 허인호
//**********23.01.12 : 몬스터 받는 데미지 파티클, hp에 따른 죽는 표현 완료- 허인호
//**********23.01.12 : 플레이어 추적 및 공격(모션만) 완성(공격 패턴 넣어야함(차후 보스나 다른 패턴있는 몬스터들 관리 편하게 하기 위해)) - 허인호
//**********23.01.11 : 몬스터 클래스 처리를 FSM으로 변경중 BT 디자인패턴으로 적용했으나 내용이 어려워 이해를 못했기에 사용은 다음에 - 허인호 
//**********23.01.09 : MonsterInfo 확인, 플레이어의 공격 및 거리에 따른 행동 제작중 - 허인호 
//**********Monster : 몬스터 클래스 : 몬스터에 관한 정보를 일단은 수동으로 정하는 클래스
/* MonsterInfo
    public int id;
    public string name;
    public int hp;
    public float moveSpeed;
    public string type;
    public string attacktType;
    public List<string> skillList;
    public int dropItemListNum;
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageabel
{
    #region State
    [HideInInspector] public MonsterBaseState currentState;
    [HideInInspector] public Monster_IdleState idle;
    [HideInInspector] public Monster_PatrolState patrol;
    [HideInInspector] public Monster_AttackState attack;
    [HideInInspector] public Monster_DiscoverMoveState discoverMove;
    [HideInInspector] public Monster_DeadState dead;
    #endregion

    [HideInInspector] public bool isBaseAttack;
    [HideInInspector] public bool isStrongAttack;
    [HideInInspector] public bool isRushAttack;
    [HideInInspector] public bool isJumpAttack;
    [HideInInspector] public bool isAOEAttack;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isApplyDamage;

    public Animator animator;
    public string monsterName;
    [HideInInspector] public GameObject player;

    [Header("LoadData")]
    [Tooltip("위에 이름 입력하면 알아서 가져옴")]
    public MonsterInfo monsterInfo;


    [Tooltip("플레이어 발견 거리")]
    public float discoverDis; //15임
    public int meleeDiscoverDis { get; set; }
    public int farDiscoverDis { get; set; }
    public int bossDiscoverDis { get; set; }

    [Tooltip("공격 거리 혹은 멈추는 거리")]
    public float stopDis;

    [Tooltip("원래 거리와의 이탈 허용 거리")]
    public float originPosDis; // 원래 거리와의 이탈 허용 거리
    public Vector3[] wayPoints { get; private set; }

    public float attackDis { get; protected set; }
    public Vector3 originPos { get; protected set; }
    public int playerLayer { get; protected set; }
    public int groundLayer { get; protected set; }


    [HideInInspector] public NavMeshAgent navAgent;
    [HideInInspector] public ATTACKTYPE currentAttackType;

    public GameObject attackColliderObj;
    [HideInInspector] public Collider attackCollider;
    bool isAttack;
    // Start is called before the first frame update

    public void Init()
    {
        idle = new Monster_IdleState();
        patrol = new Monster_PatrolState();
        discoverMove = new Monster_DiscoverMoveState();
        attack = new Monster_AttackState();
        dead = new Monster_DeadState();

        player = PlayerMovementManager.Instance.playerObj;
        monsterInfo = MonsterTableParser.Instatnce.GetMonsterData(monsterName);
        ResourceManager.Instance.LoadrcDamageEffect();
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = false;

        playerLayer = 1 << LayerMask.NameToLayer("Player");
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
       
        originPos = this.transform.position;

        if (monsterInfo.type == MONSTERTYPE.Boss) wayPoints = new Vector3[1];
        else wayPoints = new Vector3[3];

        if (attackColliderObj != null)
        {
            attackCollider = attackColliderObj.GetComponent<Collider>();
            attackColliderObj.SetActive(false);
        }

        SwitchState(idle);
        CreateWayPoint();
    }

    //공격스킬에 따라 바뀌기에 필요가 없을것으로 보임
    void BaseAttacDis(string _attackType)
    {
        /*if (_attackType == EATTACKTYPE.Melee.ToString())
        {
            attackDis = meleeDis;
        }
        else if (_attackType == EATTACKTYPE.Far.ToString())
        {
            attackDis = farDis;
        }*/
    }


    public bool Discover()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= discoverDis)
        {
            return true;
        }
        return false;
    }

    //타격받았을 시 효과와 타격 받았다는 사실 확인
    public virtual bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!isDead)
         {
            isApplyDamage = true;
            monsterInfo.hp -= damageMessage.damage;
            GameObject bloodEffectObj = ResourceManager.Instance.GetrcDamageEffect("BloodSprayEffect");
            ParticleSystem bloodEffect = bloodEffectObj.GetComponent<ParticleSystem>();
            bloodEffect = Instantiate(bloodEffect, damageMessage.hitPoint, Quaternion.LookRotation(damageMessage.hitNormal));
            bloodEffect.transform.SetParent(this.gameObject.transform);
            bloodEffect.transform.position = damageMessage.hitPoint;
            bloodEffect.Play();
        }
        if (monsterInfo.hp <= 0 && !isDead)
        {
            SwitchState(dead);
            isDead = true;
        }
         return true;
    }
    //순찰할 지점 생성
    public void CreateWayPoint()
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
    public virtual void SwitchState(MonsterBaseState state) //상태 전환 함수
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void OnAttackCollider()
    {
        attackColliderObj.SetActive(true);
        if (attackCollider.bounds.Intersects(PlayerMovementManager.Instance.playerColl.bounds))
        {
            
            var player = PlayerMovementManager.Instance.playerObj.GetComponent<IDamageabel>();
            if (player != null && !isAttack)
            {
                Debug.Log("bbb");
                isAttack = true;
                DamageMessage damageMessage;
                damageMessage.damager = PlayerMovementManager.Instance.playerObj;
                monsterInfo.damage = monsterInfo.monsterSkillList[0].damage;
                damageMessage.damage = monsterInfo.damage;
                damageMessage.hitPoint = Vector3.zero;
                damageMessage.hitNormal = Vector3.zero;
                // 상대방의 OnDamage 함수를 실행시켜서 상대방에게 데미지 주기
                player.ApplyDamage(damageMessage);
            }
        }
    }
    public void OffAttackCollider()
    {
        isAttack = false;
        attackColliderObj.SetActive(false);
    }
}
