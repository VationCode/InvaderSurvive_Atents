//**********22.12.27 : 밑의 작업 완료 - 허인호
//**********22.12.24 : 유한상태머신(FSM 디자인패턴)을 통한 걷기, 회전, 앉기, 회피기, 점프 제작중 - 허인호
//**********PlayerMovementManager : 전신 행동에 대한 매니저
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour, IDamageabel
{
    #region SingleTon
    public static PlayerMovementManager Instance;

    private void Awake()
    {
        playerObj = this.gameObject;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion

    public GameObject playerObj;
    public GameObject characterObj;
    public GameObject target; //플레이어 포지션이 바닥에 있기에 적들이 공격할 때의 공격 날라오는 포지션은 따로
    [HideInInspector]public Gun gun;
    [HideInInspector] public PickAxe pickAxe;
    private CharacterController characterController;
    #region State
    [HideInInspector] public AllBodyBaseState previousState;
    [HideInInspector] public AllBodyBaseState currentState;

    //[HideInInspector] public PlayerIdleState idle;
    [HideInInspector] public PlayerRunState run;
    [HideInInspector] public PlayerWalkState walk;
    [HideInInspector] public PlayerCrouchState crouch;
    [HideInInspector] public PlayerJumpState jump;
    [HideInInspector] public PlayerDodgeState dodge;
    [HideInInspector] public PlayerSkillEState skillE;
    [HideInInspector] public PlayerDeathState death;
    [HideInInspector] public PickAxeState2 pickAxe2 = new PickAxeState2();
    [HideInInspector] public static bool isAll; //상체 제어를 위해
    #endregion

    #region Info
    public PlayerInfo playerInfo;
    public bool isDead;
    #endregion
    #region Move
    [Header("움직임")]
    public float currentMoveSpeed;
    public float walkSeed = 3f; //애니메이션 속도에 맞추기위해 속도 조절
    public float runSeed = 7f;
    public float crouchSeed = 2f;
    public float airSpeed = 0.5f; //점프했을때 움직임 속도
    
    public Vector3 moveDirection { get; private set; }
    [SerializeField]
    private float speedSmoothTime = 0.1f; //플레이어의 움직임의 속도값 변화를 부드럽게 해주는 지연값
    private float speedSmoothVelocity; //이동값의 변화 속도
    private Vector3 velocity;
    public float currentVelocityY; //CharacterController를 사용하기에 두가지 타입의 Velocity가 필요(중력값) y값, xz값
    public float currentSpeed => new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude; //지면상의 현재 속도(크기) 계산을 위한
    #endregion

    #region Jump
    [Header("점프")]
    [SerializeField]
    LayerMask groundMask;
    public float jumpForce = 5f;
    [Tooltip("이단점프 힘")]public float jumpForce2 = 5f;
    public int jumpCount = 2;
    public float jumpT = 1.5f;
    [HideInInspector] public int currentJumpCount; //현재 점프한 카운트
    [HideInInspector] public bool isJump;
    //public void JumpForce() => velocity.y += jumpForce; //애니메이션에서 불러옴 (방식 바꿔서 필요가 없어짐)
    public void Jumped() => isJump = true; //애니메이션에서 불러옴
    #endregion

    #region Rotate
    [Header("회전")]
    [SerializeField]
    private float rotateSpeed;
    public float turnSmoothTime = 0.01f; //움직이려는 방향의 회전 속도값의 변화를 부드럽게 해주는 지연값
    private float turnSmoothVelocity; //회전값의 변화 속도
    #endregion

    #region Skill
    public bool isDodge;
    public float dodgeT = 1f;
    public float skillET = 1f;
    #endregion

    private bool isCity;
    public Collider playerColl;
    private Collider currentColl;
    private Collider prevColl;
    public bool isGunSkillE;
    //private Bounds playerBounds; // 충돌처리를 위해



    // Start is called before the first frame update
    

    void Start()
    {
        playerInfo.hp = 50;
        playerInfo.mana = 50;
        characterController = GetComponent<CharacterController>();
        characterObj = gameObject.transform.GetChild(0).gameObject;
        currentJumpCount = jumpCount;
        playerColl = GetComponent<Collider>();
        target = transform.Find("Target").gameObject;
        //idle = new PlayerIdleState();
        run = new PlayerRunState();
        walk = new PlayerWalkState();
        crouch = new PlayerCrouchState();
        jump = new PlayerJumpState();
        dodge = new PlayerDodgeState();
        skillE = new PlayerSkillEState();
        death = new PlayerDeathState();
        gun = GetComponentInChildren<Gun>();
        pickAxe = GetComponentInChildren<PickAxe>();
        SwitchState(run);
        //currentColl = GateManager.Instance.Coll[0];
        //prevColl = GateManager.Instance.Coll[0];


    }
    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Inventory.Instance.isInventoryActive)
        {
            PlayerAnimationManager.Instance.Move(0,0);
            return;
        }
        if(playerInfo.hp <= 0)
        {
            isDead = true;
        }

        Move();
        currentState.UpdateState(this);
        Rotate();
        //Gate();
    }

    public void SwitchState(AllBodyBaseState state) //상태 전환 함수
    {
        currentState = state;
        currentState.EnterState(this);
    }
    void Gate()
    {
        int count = 0;
        for (int i = 0; i < GateManager.Instance.Coll.Length; i++)
        {
            if (playerColl.bounds.Intersects(GateManager.Instance.Coll[i].bounds))
            {
                count++;
                currentColl = GateManager.Instance.Coll[i];
            }
        }
        if (count <= 1)
        {
            if (currentColl != prevColl)
            {
                prevColl = currentColl;
                UIManager.Instance.FadeInOut(prevColl.name);
                SoundManager.Instance.PlaySE(prevColl.name);
            }
        }
    }

    public void Move()
    {
        if (isDodge)
        {
            characterController.Move(characterObj.transform.forward * Time.deltaTime * 5f);
            return;
        }
        var targetSpeed = currentMoveSpeed * PlayerInput.Instance.moveInput.magnitude;

        moveDirection = Vector3.Normalize(transform.forward * PlayerInput.Instance.moveInput.y + transform.right * PlayerInput.Instance.moveInput.x); //뱡향 선정(transform을 통해 캐릭터가 회전을해도 바라보는 방향으로 앞뒤 움직임)
        targetSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime); //원래값(currentSpeed)에서 목표값(targetSpeed)으로 변화하는 직전까지의 값에 지연시간을 적용해 부드럽게 이어지도록.SmoothDamp는 값을 부드럽게 변화시킴

        //currentVelocityY += Physics.gravity.y * Time.deltaTime; //시간에 따라 중력값만큼 밑으로 계속 떨어지게 설정
        velocity = moveDirection * targetSpeed + Vector3.up * currentVelocityY; //속도를 두가지로 나누어 앞/옆 그리고 위로 따로 계산하여 마지막에 합친것(이렇게 해야 점프때 currentVelocityY로 따로 계산할 수 있음)

        characterController.Move(velocity * Time.deltaTime);

        if (IsGrounded())
        {
            //jumpT = 0;
            currentVelocityY = 0f;
        }
    }

    public bool IsGrounded() //땅에 닿았는지 확인 CharacterController에 따로 있긴하지만 성능이 그렇게 좋진않기에
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f, groundMask)) return true;
        return false;
    }

    public void Rotate()
    {
        if (isDodge) return;
        var targetRotation = Camera.main.transform.eulerAngles.y;
        targetRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        transform.eulerAngles = Vector3.up * targetRotation;
    }

    public bool ApplyDamage(DamageMessage damageMessage)
    {
        Debug.Log("damage");
        playerInfo.hp -= damageMessage.damage;
        return true;
    }

    public void OnPickAxe()
    {
        pickAxe.coll.gameObject.SetActive(true);
    }

    public void OffPickAxe()
    {
        pickAxe.coll.gameObject.SetActive(false);
    }
}
