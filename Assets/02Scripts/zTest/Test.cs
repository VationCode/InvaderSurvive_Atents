using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct City
{
    public string name;
    public Bounds size;
    public Collider FieldRegion;
}


public class Test222 : MonoBehaviour
{
    public City city;
    public City city2;
    public GameObject a;
    public Bounds bound; //Center�� ��ǥ�� , Extent�� ����(�߽����κ����� �Ÿ� ũ�� 1�Ϸ��� �߽� + 0.5)
    Collider col;
    Collider col2;
    void Start()
    {
        col = gameObject.GetComponent<Collider>();
        col2 = a.GetComponent<Collider>();
        city.FieldRegion = col;
        city2.FieldRegion = col2;
    }

    
    void Update()
    {
        bound.center = transform.position;

        if(bound.Intersects(col2.bounds))
        {
            Debug.Log("bbbbb");
        }
        if (city.FieldRegion.bounds.Intersects(col2.bounds))
        {
            Debug.Log("aaa");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        
    }
}
