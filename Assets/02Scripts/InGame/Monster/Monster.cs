//**********23.01.16 : �� ���ͺ��� ��ӹ޵���? ���� �� - ����ȣ
//**********23.01.16 : csv���Ϸκ��� ������ �޾ƿ����ϴ� ������� ���� - ����ȣ
//**********23.01.12 : ���� �޴� ������ ��ƼŬ, hp�� ���� �״� ǥ�� �Ϸ�- ����ȣ
//**********23.01.12 : �÷��̾� ���� �� ����(��Ǹ�) �ϼ�(���� ���� �־����(���� ������ �ٸ� �����ִ� ���͵� ���� ���ϰ� �ϱ� ����)) - ����ȣ
//**********23.01.11 : ���� Ŭ���� ó���� FSM���� ������ BT �������������� ���������� ������ ����� ���ظ� ���߱⿡ ����� ������ - ����ȣ 
//**********23.01.09 : MonsterInfo Ȯ��, �÷��̾��� ���� �� �Ÿ��� ���� �ൿ ������ - ����ȣ 
//**********Monster : ���� Ŭ���� : ���Ϳ� ���� ������ �ϴ��� �������� ���ϴ� Ŭ����
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
    [Tooltip("���� �̸� �Է��ϸ� �˾Ƽ� ������")]
    public MonsterInfo monsterInfo;


    [Tooltip("�÷��̾� �߰� �Ÿ�")]
    public float discoverDis; //15��
    public int meleeDiscoverDis { get; set; }
    public int farDiscoverDis { get; set; }
    public int bossDiscoverDis { get; set; }

    [Tooltip("���� �Ÿ� Ȥ�� ���ߴ� �Ÿ�")]
    public float stopDis;

    [Tooltip("���� �Ÿ����� ��Ż ��� �Ÿ�")]
    public float originPosDis; // ���� �Ÿ����� ��Ż ��� �Ÿ�
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

    //���ݽ�ų�� ���� �ٲ�⿡ �ʿ䰡 ���������� ����
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

    //Ÿ�ݹ޾��� �� ȿ���� Ÿ�� �޾Ҵٴ� ��� Ȯ��
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
    //������ ���� ����
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
            if (Physics.Raycast(new Vector3(x, 0, z), Vector3.down, out hit, Mathf.Infinity, groundLayer)) //���� ������ ���� ���� ���ϱ�
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
    public virtual void SwitchState(MonsterBaseState state) //���� ��ȯ �Լ�
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
                // ������ OnDamage �Լ��� ������Ѽ� ���濡�� ������ �ֱ�
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
