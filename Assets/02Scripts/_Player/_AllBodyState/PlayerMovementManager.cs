//**********22.12.27 : ���� �۾� �Ϸ�
//**********22.12.24 : ���ѻ��¸ӽ�(FSM ����������)�� ���� �ȱ�, ȸ��, �ɱ�, ȸ�Ǳ�, ���� ������
//**********PlayerMovementManager : ���� �ൿ�� ���� �Ŵ���
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
    public GameObject modelObj;
    public GameObject target;           // �÷��̾� �������� �ٴڿ� �ֱ⿡ ������ ������ ���� ���� ������� �������� ����
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
    [HideInInspector] public static bool isAll; //��ü ��� ����
    #endregion

    #region Info
    public PlayerInfo playerInfo;
    public bool isDead;
    #endregion
    #region Move
    [Header("������")]
    public float currentMoveSpeed;
    public float walkSeed = 3f; //�ִϸ��̼� �ӵ��� ���߱����� �ӵ� ����
    public float runSeed = 7f;
    public float crouchSeed = 2f;
    public float airSpeed = 0.5f; //���������� ������ �ӵ�
    
    public Vector3 moveDirection { get; private set; }
    [SerializeField]
    private float speedSmoothTime = 0.1f; //�÷��̾��� �������� �ӵ��� ��ȭ�� �ε巴�� ���ִ� ������
    private float speedSmoothVelocity; //�̵����� ��ȭ �ӵ�
    private Vector3 velocity;
    public float currentVelocityY; //CharacterController�� ����ϱ⿡ �ΰ��� Ÿ���� Velocity�� �ʿ�(�߷°�) y��, xz��
    public float currentSpeed => new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude; //������� ���� �ӵ�(ũ��) ����� ����
    #endregion

    #region Jump
    [Header("����")]
    [SerializeField]
    LayerMask groundMask;
    public float jumpForce = 5f;
    [Tooltip("�̴����� ��")]public float jumpForce2 = 5f;
    public int jumpCount = 2;
    public float jumpT = 1.5f;
    [HideInInspector] public int currentJumpCount; //���� ������ ī��Ʈ
    [HideInInspector] public bool isJump;
    //public void JumpForce() => velocity.y += jumpForce; //�ִϸ��̼ǿ��� �ҷ��� (��� �ٲ㼭 �ʿ䰡 ������)
    public void Jumped() => isJump = true; //�ִϸ��̼ǿ��� �ҷ���
    #endregion

    #region Rotate
    [Header("ȸ��")]
    [SerializeField]
    private float rotateSpeed;
    public float turnSmoothTime = 0.01f; //�����̷��� ������ ȸ�� �ӵ����� ��ȭ�� �ε巴�� ���ִ� ������
    private float turnSmoothVelocity; //ȸ������ ��ȭ �ӵ�
    #endregion

    #region Skill
    public bool isDodge;
    public float dodgeT = 1f;
    public float skillET = 1f;
    #endregion

    private bool isCity;
    public Collider playerColl { get; private set; }
    private Collider currentColl;
    private Collider prevColl;
    public bool isGunSkillE;
    //private Bounds playerBounds; // �浹ó���� ����

    // Start is called before the first frame update
    

    void Start()
    {
        playerInfo.hp = 50;
        playerInfo.mana = 50;
        characterController = GetComponent<CharacterController>();
        modelObj = gameObject.transform.GetChild(0).gameObject;
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
        Rotate();

        currentState.UpdateState(this);
    }

    public void SwitchState(AllBodyBaseState state) //���� ��ȯ �Լ�
    {
        currentState = state;
        currentState.EnterState(this);
    }
    
    public void Move()
    {
        if (isDodge)
        {
            characterController.Move(modelObj.transform.forward * Time.deltaTime * 5f);
            return;
        }
        var targetSpeed = currentMoveSpeed * PlayerInput.Instance.moveInput.magnitude;

        moveDirection = Vector3.Normalize(transform.forward * PlayerInput.Instance.moveInput.y + transform.right * PlayerInput.Instance.moveInput.x); //���� ����(transform�� ���� ĳ���Ͱ� ȸ�����ص� �ٶ󺸴� �������� �յ� ������)
        targetSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime); //������(currentSpeed)���� ��ǥ��(targetSpeed)���� ��ȭ�ϴ� ���������� ���� �����ð��� ������ �ε巴�� �̾�������.SmoothDamp�� ���� �ε巴�� ��ȭ��Ŵ

        //currentVelocityY += Physics.gravity.y * Time.deltaTime; //�ð��� ���� �߷°���ŭ ������ ��� �������� ����

        if (IsGrounded())
        {
            currentVelocityY = 0f;
        }
        else
        {
            currentVelocityY += Physics.gravity.y * Time.deltaTime;
        }

        velocity = moveDirection * targetSpeed + Vector3.up * currentVelocityY; //�ӵ��� �ΰ����� ������ ��/�� �׸��� ���� ���� ����Ͽ� �������� ��ģ��(�̷��� �ؾ� ������ currentVelocityY�� ���� ����� �� ����)

        characterController.Move(velocity * Time.deltaTime);

        
    }

    public bool IsGrounded() //���� ��Ҵ��� Ȯ�� CharacterController�� ���� �ֱ������� ������ �׷��� �����ʱ⿡
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
