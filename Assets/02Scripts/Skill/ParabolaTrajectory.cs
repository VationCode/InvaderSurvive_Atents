using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//포물선 궤적 계산
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

    //속력 계산 하니깐 이상해짐 계산이 잘못된건지는 몰라도....
    /*void CalculationVelocity(float _degree) //시간에 따른 속도변화
    {
        Debug.Log("속도 = " + velocity + "각도 = " + _degree + "중력값 = " + gravity);
        //velocity = (velocity * Mathf.Sin(_degree * Mathf.Deg2Rad)) - (gravity * Time.deltaTime);
        CalculationFlightTime(velocity, _degree);
    }*/

    void CalculationFlightTime(float _degree) //체공시간(내손에서 벗어나 날아가는 시간), 딱히 필요가 없음
    {
        // 최고점까지 가는시간과 땅에 떨어진시간은 비례하기에 * 2
        //Mathf.Sin(_degree * Mathf.Deg2Rad)는 각도에 따른 높이를 계산한것

        flightTime = velocity / gravity; // 최고점 가는 속도
        flightTime = ((velocity * Mathf.Sin(_degree * Mathf.Deg2Rad)) * gravity) * 2;
        CalculationRDistance(_degree);
    }

    void CalculationRDistance(float _degree) //수평도달거리
    {
        //참고 45도로 던졌을때 가장 멀리감
        //Mathf.Pow()는 제곱근 함수
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
        centerDis.y -= slerpOffset; //Slerp를 위방향으로 만들기 위해 y값을 -시켜준다.(잘 이해는 가지가 않음;) 무튼 Slerp의 높이 조절 가능
        StartPos = StartPos - centerDis;
        _endPos = _endPos - centerDis;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 point = Vector3.Slerp(StartPos, _endPos, i / (float)(lineRenderer.positionCount - 1)) + centerDis;
            //문제점 - 부딪힌 건물의 위에 생성이 되어야하는 기술이 필요, 시간이 없으므로 차후 해결
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
            //lineRenderer.SetPosition(i, point);//각 라인랜더러의 포지션들 설정
        }
    }
    void CalculationHeightPosition(Vector3 _endPos, float _velocity, float _degree) //최고점의 높이(Slerp때문에 딱히 필요가 없는듯)
    {
        heightPosition = (Mathf.Pow(_velocity, 2) * Mathf.Pow(Mathf.Sin(_degree * Mathf.Deg2Rad), 2)) / (2 * gravity);
        Vector3 StartPos = linePos.transform.position;
        Vector3 centerDis;
        centerDis = (StartPos + _endPos) * 0.5f;
        heightPos = new Vector3(centerDis.x, heightPosition, centerDis.z); //최고점 위치
    }

    // Update is called once per frame
    void Update()
    {
        degree = Camera.main.transform.eulerAngles.x;

        if (degree >= 270) //아래볼때 멈추게하는거 수정 필요 어렵기도하고 시간 부족한 관계로 이대로 진행
        {
            degree -= 360f;
            degree = degree * -1f;
        }
        /*if (PlayerSkill.Instance.isGunSkillE)
        {
            //lineRenderer.enabled = true;
            CalculationRDistance(degree + 5); //+5 이유는 플레이어와 겹치게 하지 않기 위해 거리 조절한것
        }
        else if (!PlayerSkill.Instance.isGunSkillE)
        {
            if (skillRange != null) Destroy(skillRange);
            //lineRenderer.enabled = false;
        }*/
    }
}
