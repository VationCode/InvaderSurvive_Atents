//**********23.01.01 : 필드 위에 생성된 아이템들 떠다니고 회전 및 파티클 효과
//**********FieldItemEffect : 필드 위 아이템 효과 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FieldItemEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject m_particle;
    public float rotSpeed;
    private void Start()
    {
        rotSpeed = 20f;
        CreateParticle();
    }
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 pos = hit.point;
            pos.y += 1f;
            transform.position = pos;
        }
        else if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 pos = hit.point;
            pos.y += 1f;
            transform.position = pos;
        }

        transform.rotation = Quaternion.Euler(0,Time.time * rotSpeed,0);
    }

    //아이템이 필드에 생성 시 공중에서 회전 및 파티클 생성
    void CreateParticle()
    {
        //GameObject particle = Resources.Load<GameObject>("03rcParticles/FieldItemEffect/_Prefab/Eff_Heal_2");

        GameObject particle = Instantiate(m_particle, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.rotation);
        particle.transform.SetParent(this.gameObject.transform);
    }
}
