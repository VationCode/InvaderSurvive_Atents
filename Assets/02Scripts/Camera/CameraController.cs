//**********22.11.28 : 마우스 회전에 따른 카메라의 회전 및 캐릭터와 카메라 사이 장애물 있을경우 거리 좁히기 - 허인호
//**********CameraController : 카메라 컨트롤러 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform followObj;
    [SerializeField]
    private float followSpeed = 10f;
    [SerializeField]
    private float sensitivity = 100; //마우스 감도
    [SerializeField]
    private float clampAngle = 70f; //카메라의 x축앵글 허용범위
    [SerializeField]
    private Transform realCamera;
    [SerializeField]
    private Vector3 dirNormalized;
    [SerializeField]
    private Vector3 finalDir;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float finalDistance;
    [SerializeField]
    private float smothness;

    private float rotX;
    private float rotY;


    private void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;
    }
    private void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX,-clampAngle, clampAngle); 
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, followObj.position, Time.deltaTime * followSpeed);
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        //카메라와 캐릭터간의 장애물이 있을경우
        RaycastHit hit;
        if(Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smothness);
    }
}
