//**********23.01.02 : 리로드 상태 추가(리로드 미완성 작업중)
//**********22.12.28 : 대기 에임상태 및 발사 시 줌효과
//**********UpperStateManager : 상반신 제어 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class UpperStateManager : MonoBehaviour
{

    [HideInInspector]public Gun gun;
    #region State
    public UpperBaseState currentState;
    public RifleIDleState RifleIdle = new RifleIDleState();
    public RifleAimFireState Aim = new RifleAimFireState();
    public ReloadState reload = new ReloadState();
    public PickAxeState pickAxeState = new PickAxeState();
    #endregion

    #region RifleAim
    [HideInInspector] public float currentFov;
    public float adsFov = 50; // 사격시 줌상태
    //public float adsFov = 60; // 사격시 줌상태
    public float IdleFov; // 평상시 줌상태
    public float fovSmoothSpeed = 10;
    public float aimPoseT;
    public float reloadT;
    #endregion

    private CinemachineVirtualCamera vCam;
    public PickAxe pickAxe { get; private set; }
   //[HideInInspector] public Rig rig;
    [HideInInspector] public static MultiAimConstraint[] multiAimConstraintData; //(애니메이션 동작 손 위치 때문에 넣음)
    void Start()
    {
        aimPoseT = 2.5f;
        reloadT = 1.5f;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        IdleFov = vCam.m_Lens.FieldOfView;
        //rig = GetComponentInChildren<Rig>(); //차후 한번에 닫으려고했으나 머리 애니메이션이 이상해서 사용x
        multiAimConstraintData = GetComponentsInChildren<MultiAimConstraint>();
        currentFov = IdleFov;
        gun = GetComponentInChildren<Gun>();
        pickAxe = GetComponentInChildren<PickAxe>();
        pickAxe.gameObject.SetActive(false);
        SwitchState(RifleIdle);
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.Instance.isInventoryActive) return;
        RifleFireZoom();
        currentState.UpdateState(this);
    }

    void RifleFireZoom()
    {
        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);
    }

    public void SwitchState(UpperBaseState state)
    {

        currentState = state;
        currentState.EnterState(this);
    }
}
