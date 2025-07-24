//**********22.11.28 : ���콺 ȸ���� ���� ī�޶��� ȸ�� �� ĳ���Ϳ� ī�޶� ���� ��ֹ� ������� �Ÿ� ������ - ����ȣ
//**********CameraController : ī�޶� ��Ʈ�ѷ� Ŭ����
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
    private float sensitivity = 100; //���콺 ����
    [SerializeField]
    private float clampAngle = 70f; //ī�޶��� x��ޱ� ������
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

        //ī�޶�� ĳ���Ͱ��� ��ֹ��� �������
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
