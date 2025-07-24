using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    public float speed;
    int layer;
    Vector3 target;
    Vector3 dir;
    float destoryT;
    private void Start()
    {
        target = PlayerMovementManager.Instance.target.transform.position;
        dir = target - transform.position;
        dir = dir.normalized;
        layer = 1 << LayerMask.NameToLayer("Monster");
        Destroy(gameObject, 10f);
    }
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.layer.Equals(layer)) //������ ������ �ٸ� ���̾ �ε����� ���
        {
            Destroy(this.gameObject);
        }
    }
}
