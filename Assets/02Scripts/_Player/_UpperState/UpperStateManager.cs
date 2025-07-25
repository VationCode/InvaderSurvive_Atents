//**********23.01.02 : ���ε� ���� �߰�(���ε� �̿ϼ� �۾���)
//**********22.12.28 : ��� ���ӻ��� �� �߻� �� ��ȿ��
//**********UpperStateManager : ��ݽ� ���� Ŭ����
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
    public float adsFov = 50; // ��ݽ� �ܻ���
    //public float adsFov = 60; // ��ݽ� �ܻ���
    public float IdleFov; // ���� �ܻ���
    public float fovSmoothSpeed = 10;
    public float aimPoseT;
    public float reloadT;
    #endregion

    private CinemachineVirtualCamera vCam;
    public PickAxe pickAxe { get; private set; }
   //[HideInInspector] public Rig rig;
    [HideInInspector] public static MultiAimConstraint[] multiAimConstraintData; //(�ִϸ��̼� ���� �� ��ġ ������ ����)
    void Start()
    {
        aimPoseT = 2.5f;
        reloadT = 1.5f;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        IdleFov = vCam.m_Lens.FieldOfView;
        //rig = GetComponentInChildren<Rig>(); //���� �ѹ��� �������������� �Ӹ� �ִϸ��̼��� �̻��ؼ� ���x
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
