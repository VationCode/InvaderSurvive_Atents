//**********23.01.12 : ���� �޴� ������ ��ƼŬ, hp�� ���� �״� ǥ�� �Ϸ�
//**********23.01.12 : �÷��̾� ���� �� ����(��Ǹ�) �ϼ�(���� ���� �־����(���� ������ �ٸ� �����ִ� ���͵� ���� ���ϰ� �ϱ� ����))
//**********23.01.11 : ���� Ŭ���� ó���� FSM���� ������ BT �������������� ���������� ������ ����� ���ظ� ���߱⿡ ����� ������
//**********23.01.09 : MonsterInfo Ȯ��, �÷��̾��� ���� �� �Ÿ��� ���� �ൿ ������
//**********Monster : ���� Ŭ���� : ���Ϳ� ���� ������ �ϴ��� �������� ���ϴ� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterT : MonoBehaviour, IDamageabel
{
   
    public GameObject player;
    public MonsterInfos monsterInfos;
    public Vector3[] wayPoints;
  
    [Tooltip("�÷��̾� �߰� �Ÿ�")]
    [SerializeField] private float discoverDis;
    [Tooltip("���� �Ÿ� Ȥ�� ���ߴ� �Ÿ�")]
    public float stopDis;
    [Tooltip("���� �Ÿ����� ��Ż ��� �Ÿ�")]
    public float originPosDis;

    [SerializeField]
    private GameObject m_bloodSprayEffect;

    /*[Tooltip("���� �ڸ��κ��� ��Ż�� �� �ִ� �Ÿ�")]
    [SerializeField] private float originPosBreakAwayDis;
    [Tooltip("�÷��̾�� ���Ͱ� ��Ż �Ÿ�")]
    [SerializeField] private float playerPosBreakAwayDis;*/

    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public bool isApplyDamage;
    [HideInInspector] public bool isDead;

    [HideInInspector]public Vector3 originPos;
    [HideInInspector] public Quaternion originRot;
    [HideInInspector] public MonsterBaseStateT currentState;
    [HideInInspector] public Monster_PatrolState patrol; //��������Ʈ�� ����
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



    public bool Discover()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= discoverDis) return true;
        return false;
    }

    public void CreateMonster()
    {

    }
    public void SwitchState(MonsterBaseStateT state) //���� ��ȯ �Լ�
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public virtual bool ApplyDamage(DamageMessage damageMessage)
    {
        //if (damageMessage.damager == gameObject) return false;
        //����� ���
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
