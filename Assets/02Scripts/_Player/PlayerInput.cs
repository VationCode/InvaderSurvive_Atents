//**********22.12.20 : crouch(�ɱ�) �߰�
//**********22.12.17 : pickAxe(���) �߰�
//**********22.10.11 : ButtonOnPointerEvent��ũ��Ʈ�� �� ��ư�鿡 �߰��Ͽ� OnPointerEvent�� ���� ���� �޾ƾƿ�
//**********22.10.05 : ��ư�� ���̽�ƽ�� ��������
//**********PlayerInput : �÷��̾� ���� ���� ���� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class PlayerInput : MonoBehaviour
{
    #region SingleTon
    static public PlayerInput Instance;

    #endregion
    // TODO : �ȵ���̵� ��ȯ �۾�
    /*[SerializeField]
    private Button[] btnArray;*/

    public Vector2 moveInput { get; private set; }
    public Vector3 rotateInput { get; private set; }
    public bool isAttack { get; private set; }  // MouseL
    public bool isCrouch { get; private set; }  // C
    public bool isSword { get; private set; }   //1
    public bool isGun { get; private set; }     //2
    public bool isPickAxe { get; private set; } //3
    public bool isJump { get; private set; }    //Space
    public bool isReload { get; private set; }  //R
    public bool isRun { get; private set; }     //�⺻���� �ٱ�(���̽�ƽ���δ� ����)

    //��ų�ι�
    public bool isDodge { get; private set; } // Shift ȸ�Ǳ� ��ų
    public bool isSkillQ { get; private set; } // Q �� ª�� ��ų
    public bool isSkillE { get; private set; } // E ������ų(AoE)

    private string horizontal = "Horizontal";
    private string vertical = "Vertical";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Update()
    {
        //if (Inventory.inventoryActivated) return;
        //PC Ű���� �������̽� ���
        moveInput = new Vector2(Input.GetAxis(horizontal), Input.GetAxis(vertical));
        rotateInput = new Vector2(-Input.GetAxis("Mouse Y") * 2.0f, Input.GetAxis("Mouse X") * 2.0f);
        if (moveInput.sqrMagnitude > 1) //�ﰢ�Լ��� ���ؼ� �밢������ �̵��� �� �ӵ��� �� ���� �����Ѵ�. ���� ����ȭ ������ �ʿ䰡 �ִ�.
        {
            moveInput = moveInput.normalized;
        }

        isJump = Input.GetKeyDown(KeyCode.Space);
        isCrouch = Input.GetKeyDown(KeyCode.C);
        //����
        isAttack = Input.GetMouseButton(0);
        isReload = Input.GetKeyDown(KeyCode.R);

        //���ⱳü
        isSword = Input.GetKeyDown(KeyCode.Alpha1);
        isGun = Input.GetKeyDown(KeyCode.Alpha2);
        isPickAxe = Input.GetKeyDown(KeyCode.Alpha3);
        
        //��ų
        isDodge = Input.GetKeyDown(KeyCode.LeftShift);
        isSkillQ = Input.GetKeyDown(KeyCode.Q);
        isSkillE = Input.GetKeyDown(KeyCode.E);

        /*// ����� ��ư �������̽� ���
        moveInput = JoystickInput.moveInputDir; //���̽�ƽ ��
        rotateInput = JoystickInput.rotateInputDir; //���̽�ƽ ��
        isAttack = btnOnPoint[0].isOnClick;
        isChangeWeapon[0] = btnOnPoint[1].isOnClick;
        isChangeWeapon[1] = btnOnPoint[2].isOnClick;
        isChangeWeapon[2] = btnOnPoint[3].isOnClick;
        isJump = btnOnPoint[4].isOnClick;
        isReolad = btnOnPoint[5].isOnClick;*/

    }
}
