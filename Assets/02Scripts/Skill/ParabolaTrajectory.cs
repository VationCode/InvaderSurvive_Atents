using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ���� ���
public class parabolaTrajectory : MonoBehaviour
{
    public GameObject linePos;
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    [Range(0f, 100f)]
    float slerpOffset;


    public static Vector3 endPos { get; private set; }
    float velocity = 20f;
    float gravity = 9.8f;
    float flightTime;
    float rDis;
    float heightPosition;
    Vector3 heightPos;
    float degree;
    GameObject skillRange;

    void Start()
    {
        //skillRange = Resources.Load<GameObject>("Models/Particle/RangeSkill/Range");
        //skillRange.name = "EndPos";
        //lineRenderer.positionCount = 10;
        //lineRenderer.enabled = false;

    }

    //�ӷ� ��� �ϴϱ� �̻����� ����� �߸��Ȱ����� ����....
    /*void CalculationVelocity(float _degree) //�ð��� ���� �ӵ���ȭ
    {
        Debug.Log("�ӵ� = " + velocity + "���� = " + _degree + "�߷°� = " + gravity);
        //velocity = (velocity * Mathf.Sin(_degree * Mathf.Deg2Rad)) - (gravity * Time.deltaTime);
        CalculationFlightTime(velocity, _degree);
    }*/

    void CalculationFlightTime(float _degree) //ü���ð�(���տ��� ��� ���ư��� �ð�), ���� �ʿ䰡 ����
    {
        // �ְ������� ���½ð��� ���� �������ð��� ����ϱ⿡ * 2
        //Mathf.Sin(_degree * Mathf.Deg2Rad)�� ������ ���� ���̸� ����Ѱ�

        flightTime = velocity / gravity; // �ְ��� ���� �ӵ�
        flightTime = ((velocity * Mathf.Sin(_degree * Mathf.Deg2Rad)) * gravity) * 2;
        CalculationRDistance(_degree);
    }

    void CalculationRDistance(float _degree) //���򵵴ްŸ�
    {
        //���� 45���� �������� ���� �ָ���
        //Mathf.Pow()�� ������ �Լ�
        rDis = (Mathf.Pow(velocity, 2) * Mathf.Sin((2 * _degree) * Mathf.Deg2Rad)) / gravity;
        endPos = linePos.transform.position + linePos.transform.forward * rDis;

        RaycastHit hitInfo;
        if (Physics.Raycast(endPos, Vector3.down, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider.CompareTag("Building"))
            {
                endPos = hitInfo.point;
                //CalculationHeightPosition(endPos, velocity, _degree);
                SetSlerp(endPos);
            }
        }
        else
        {
            SetSlerp(endPos);
        }
    }

    void SetSlerp(Vector3 _endPos)
    {
        Vector3 StartPos = linePos.transform.position;
        Vector3 centerDis;
        centerDis = (StartPos + _endPos) * 0.5f;
        centerDis.y -= slerpOffset; //Slerp�� ���������� ����� ���� y���� -�����ش�.(�� ���ش� ������ ����;) ��ư Slerp�� ���� ���� ����
        StartPos = StartPos - centerDis;
        _endPos = _endPos - centerDis;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 point = Vector3.Slerp(StartPos, _endPos, i / (float)(lineRenderer.positionCount - 1)) + centerDis;
            //������ - �ε��� �ǹ��� ���� ������ �Ǿ���ϴ� ����� �ʿ�, �ð��� �����Ƿ� ���� �ذ�
            /*RaycastHit hitInfo;
            if(Physics.Raycast(point, Vector3.forward, out hitInfo, 1f))
            {
                if(hitInfo.collider.CompareTag("Building"))
                {
                    point = hitInfo.point;
                }
            }*/
            if (skillRange == null)
            {
                skillRange = Resources.Load<GameObject>("Models/Particle/RangeSkill/Range");
                skillRange = Instantiate(skillRange);
            }
            skillRange.transform.position = point;
            //lineRenderer.SetPosition(i, point);//�� ���η������� �����ǵ� ����
        }
    }
    void CalculationHeightPosition(Vector3 _endPos, float _velocity, float _degree) //�ְ����� ����(Slerp������ ���� �ʿ䰡 ���µ�)
    {
        heightPosition = (Mathf.Pow(_velocity, 2) * Mathf.Pow(Mathf.Sin(_degree * Mathf.Deg2Rad), 2)) / (2 * gravity);
        Vector3 StartPos = linePos.transform.position;
        Vector3 centerDis;
        centerDis = (StartPos + _endPos) * 0.5f;
        heightPos = new Vector3(centerDis.x, heightPosition, centerDis.z); //�ְ��� ��ġ
    }

    // Update is called once per frame
    void Update()
    {
        degree = Camera.main.transform.eulerAngles.x;

        if (degree >= 270) //�Ʒ����� ���߰��ϴ°� ���� �ʿ� ��Ʊ⵵�ϰ� �ð� ������ ����� �̴�� ����
        {
            degree -= 360f;
            degree = degree * -1f;
        }
        /*if (PlayerSkill.Instance.isGunSkillE)
        {
            //lineRenderer.enabled = true;
            CalculationRDistance(degree + 5); //+5 ������ �÷��̾�� ��ġ�� ���� �ʱ� ���� �Ÿ� �����Ѱ�
        }
        else if (!PlayerSkill.Instance.isGunSkillE)
        {
            if (skillRange != null) Destroy(skillRange);
            //lineRenderer.enabled = false;
        }*/
    }
}
