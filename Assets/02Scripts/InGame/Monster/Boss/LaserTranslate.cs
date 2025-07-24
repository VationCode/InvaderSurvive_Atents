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
        if (other.gameObject.layer.Equals(layer)) //본인을 제외한 다른 레이어에 부딛혔을 경우
        {
            Destroy(this.gameObject);
        }
    }
}
