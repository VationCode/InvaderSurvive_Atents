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
    [SerializeField] public Transform aimPos; //������ �������� �׳� �ƹ��ų� �����
    [SerializeField] LayerMask aimMask; //ī�޶󿡼� ���̹߻� �� ������ ���̾� ����
    [SerializeField] float aimSmoothSpeed = 20;
    #endregion

    #region Camera
    [SerializeField] private Transform camFollowPos;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float mouseSense = 1; //���콺 ����
   
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

    //ī�޶� ������ �ٷ�
    void calculate()
    {
        xAxis = Input.GetAxisRaw("Mouse X") * mouseSense;
        // ���������� Y�� ȸ���� ���Ѱ� �ݴ�ΰ� �ͼ���(���콺 �����ϸ� ȭ�� ����)
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

    void MoveCamera() //�ɾ������� �¿����
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