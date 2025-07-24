using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyTest : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("a");
        }
    }
}
