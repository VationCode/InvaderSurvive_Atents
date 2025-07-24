//**********23.01.07 : 보스 몬스터 관련하여 제작중(패턴 관련해서) - 허인호 
//**********BossManager : 보스 매니저 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossManager : MonoBehaviour
{
    enum EPattern //보스 애니메이션 나열
    {
        Idle,
        Walk,
        Attack1,
        Attack2,
        LaserAttack,
        JumpAttack,
    }
    #region State
    [HideInInspector] public Boss_BaseState currentState;

    [HideInInspector] public Boss_UnDiscoverState unDiscover;
    [HideInInspector] public Boss_IdleState idle;
    [HideInInspector] public Boss_WalkState walk;
    [HideInInspector] public Boss_Attack1State attack1;
    [HideInInspector] public Boss_Attack2State attack2;
    [HideInInspector] public Boss_LaserState laser;
    [HideInInspector] public Boss_JumpAttackState jumpAttack;
    #endregion
    
    [HideInInspector] public Animator animator;
    //[HideInInspector] public LaserAttack laserAttack;


    public MonsterInfos monsterInfo;

    public GameObject invaderRange;

    public bool isDiscover { get; private set; }
    public Collider coll;
    public Collider playerColl;

    public float distance;
    [HideInInspector] public float dis;
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public bool isWalk;
    void Start()
    {
        animator = GetComponent<Animator>();
        coll = invaderRange.GetComponent<Collider>();
        playerColl = PlayerMovementManager.Instance.playerObj.GetComponent<Collider>();
        
        
        
        
        
        
        
        //laserAttack = new LaserAttack();
        unDiscover = new Boss_UnDiscoverState();
        idle = new Boss_IdleState();
        walk = new Boss_WalkState();
        attack1 = new Boss_Attack1State();
        attack2 = new Boss_Attack2State();
        laser = new Boss_LaserState();
        jumpAttack = new Boss_JumpAttackState();




        //SwitchState(unDiscover);
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(PlayerMovementManager.Instance.playerObj.transform.position,transform.position);
        if(isDiscover)
        {
            if (dis >= distance)
            {
                isWalk = true;
            }
            else isWalk = false;
        }
        if (coll.bounds.Intersects(playerColl.bounds) && !isDiscover)
        {
            isDiscover = true;
        }
        //currentState.UpdateState(this);
    }

    public void SwitchState(Boss_BaseState state) //상태 전환 함수
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
