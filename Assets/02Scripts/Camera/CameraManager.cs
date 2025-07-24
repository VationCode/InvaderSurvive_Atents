using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    #region SingleTon
    public static CameraManager Instance;

    private void Awake()
    {
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

    #region Aim
    [SerializeField] public Transform aimPos; //눈으로 보기위해 그냥 아무거나 만든것
    [SerializeField] LayerMask aimMask; //카메라에서 레이발사 시 적용할 레이어 선별
    [SerializeField] float aimSmoothSpeed = 20;
    #endregion

    #region Camera
    [SerializeField] private Transform camFollowPos;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float mouseSense = 1; //마우스 감도
   
    float xAxis, yAxis;
    private Vector3 dirNormalized;
    #endregion
    float xFollowPos;
    float yFollowPos, ogYPos;
    [SerializeField] float crouchCamHeight = 0.6f;
    [SerializeField] float shoulderWapSpeed = 10;

    private void Start()
    {
        xFollowPos = camFollowPos.localPosition.x;
        ogYPos = camFollowPos.localPosition.y;
        yFollowPos = ogYPos;
    }
    private void Update()
    {
        if (Inventory.Instance.isInventoryActive) return;
        calculate();
        MoveCamera();
    }

    private void LateUpdate()
    {
        UpdateCursorLockMode();
        if (Inventory.Instance.isInventoryActive) return;
        Rotate();
    }

    //카메라 정보만 다룸
    void calculate()
    {
        xAxis = Input.GetAxisRaw("Mouse X") * mouseSense;
        // 국내에서는 Y축 회전에 대한게 반대로가 익숙함(마우스 위로하면 화면 위로)
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -60, 60);
        
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }
    }

    void Rotate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, xAxis, camFollowPos.localEulerAngles.z);
    }

    void MoveCamera() //앉았을때와 좌우반전
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) xFollowPos = -xFollowPos;
        if (PlayerMovementManager.Instance.currentState == PlayerMovementManager.Instance.crouch) yFollowPos = crouchCamHeight;
        else yFollowPos = ogYPos;

        Vector3 newFollowPos = new Vector3(xFollowPos, yFollowPos, camFollowPos.localPosition.z);
        camFollowPos.localPosition = Vector3.Lerp(camFollowPos.localPosition, newFollowPos, shoulderWapSpeed * Time.deltaTime); ;
    }

    private void UpdateCursorLockMode()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}