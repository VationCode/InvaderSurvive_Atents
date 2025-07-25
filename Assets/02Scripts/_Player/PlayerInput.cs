//**********22.12.20 : crouch(앉기) 추가
//**********22.12.17 : pickAxe(곡괭이) 추가
//**********22.10.11 : ButtonOnPointerEvent스크립트를 각 버튼들에 추가하여 OnPointerEvent에 대한 정보 받아아옴
//**********22.10.05 : 버튼과 조이스틱의 정보전달
//**********PlayerInput : 플레이어 동작 정보 전달 클래스
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
    // TODO : 안드로이드 전환 작업
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
    public bool isRun { get; private set; }     //기본상태 뛰기(조이스틱으로는 설정)

    //스킬부문
    public bool isDodge { get; private set; } // Shift 회피기 스킬
    public bool isSkillQ { get; private set; } // Q 쿨 짧은 스킬
    public bool isSkillE { get; private set; } // E 범위스킬(AoE)

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
        //PC 키보드 인터페이스 방식
        moveInput = new Vector2(Input.GetAxis(horizontal), Input.GetAxis(vertical));
        rotateInput = new Vector2(-Input.GetAxis("Mouse Y") * 2.0f, Input.GetAxis("Mouse X") * 2.0f);
        if (moveInput.sqrMagnitude > 1) //삼각함수에 의해서 대각선으로 이동할 때 속도가 더 많이 증가한다. 따라서 정규화 시켜줄 필요가 있다.
        {
            moveInput = moveInput.normalized;
        }

        isJump = Input.GetKeyDown(KeyCode.Space);
        isCrouch = Input.GetKeyDown(KeyCode.C);
        //공격
        isAttack = Input.GetMouseButton(0);
        isReload = Input.GetKeyDown(KeyCode.R);

        //무기교체
        isSword = Input.GetKeyDown(KeyCode.Alpha1);
        isGun = Input.GetKeyDown(KeyCode.Alpha2);
        isPickAxe = Input.GetKeyDown(KeyCode.Alpha3);
        
        //스킬
        isDodge = Input.GetKeyDown(KeyCode.LeftShift);
        isSkillQ = Input.GetKeyDown(KeyCode.Q);
        isSkillE = Input.GetKeyDown(KeyCode.E);

        /*// 모바일 버튼 인터페이스 방식
        moveInput = JoystickInput.moveInputDir; //조이스틱 값
        rotateInput = JoystickInput.rotateInputDir; //조이스틱 값
        isAttack = btnOnPoint[0].isOnClick;
        isChangeWeapon[0] = btnOnPoint[1].isOnClick;
        isChangeWeapon[1] = btnOnPoint[2].isOnClick;
        isChangeWeapon[2] = btnOnPoint[3].isOnClick;
        isJump = btnOnPoint[4].isOnClick;
        isReolad = btnOnPoint[5].isOnClick;*/

    }
}
