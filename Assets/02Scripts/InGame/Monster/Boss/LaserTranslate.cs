using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTranslate : MonoBehaviour
{
    int layer;
    
    void Start()
    {
        layer = 1 << LayerMask.NameToLayer("Player");
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 10 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(layer)) //������ ������ �ٸ� ���̾ �ε����� ���
        {
            Destroy(this.gameObject);
        }
    }
}
