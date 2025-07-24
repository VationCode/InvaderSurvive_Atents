//**********23.01.01 : �ʵ� ���� ������ �����۵� ���ٴϰ� ȸ�� �� ��ƼŬ ȿ�� - ����ȣ 
//**********FieldItemEffect : �ʵ� �� ������ ȿ�� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItemEffect : MonoBehaviour
{
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

    //�������� �ʵ忡 ���� �� ���߿��� ȸ�� �� ��ƼŬ ����
    void CreateParticle()
    {
        GameObject particle = Resources.Load<GameObject>("03rcParticles/FieldItemEffect/_Prefab/Eff_Heal_2");
        particle = Instantiate(particle, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.rotation);
        particle.transform.SetParent(this.gameObject.transform);
    }
}
