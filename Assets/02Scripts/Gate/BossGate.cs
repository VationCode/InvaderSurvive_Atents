using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public GameObject dolyCam;
    public Monster monster;
    Collider playerCol;
    Collider coll;
    Bounds bound;
    void Start()
    {
        playerCol = PlayerMovementManager.Instance.playerObj.GetComponent<Collider>();
        coll = GetComponent<Collider>();
        bound = coll.bounds;
        bound.center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bound.Intersects(playerCol.bounds))
        {
            dolyCam.SetActive(true);
            monster.animator.SetBool("IsWakeUp", true);
        }
        //else dolyCam.SetActive(false);
    }
}
