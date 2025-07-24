using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandTracking : MonoBehaviour
{
    public void HandTracking(GameObject _handAttach)
    {
        transform.position = _handAttach.transform.position;
        transform.rotation = _handAttach.transform.rotation;
    }

}
