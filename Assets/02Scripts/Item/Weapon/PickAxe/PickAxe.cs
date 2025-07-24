using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : MonoBehaviour
{
    public WeaponInfo weaponInfo;

    private const string rightAttachNode = "----Model----/Player/Character/Vanguard By T. Choonyung/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/GunRightAttach";
    private GameObject rightAttach;
    RightHandTracking rightHandTracking;

    public Collider coll;
    public GameObject[] crystal;
    Collider[] crystalColl;

    bool isTouch;
    float GatheringT;
    void Start()
    {
        rightHandTracking = GetComponent<RightHandTracking>();
        rightAttach = GameObject.Find(rightAttachNode);
        coll = GetComponentInChildren<Collider>();
        coll.gameObject.SetActive(false);
        crystal = GameObject.FindGameObjectsWithTag("Crystal");
        crystalColl = new Collider[crystal.Length];
        for (int i = 0; i < crystalColl.Length; i++)
        {
            crystalColl[i] = crystal[i].GetComponent<Collider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < crystalColl.Length; i++)
        {
            if(coll.bounds.Intersects(crystalColl[i].bounds))
            {
                if (!isTouch)
                {
                    Debug.Log("bbb");
                    crystalColl[i].GetComponent<Crystal>().Mining();
                    isTouch = true;
                }
            }
        }
        if(isTouch)
        {
            GatheringT += Time.deltaTime;
            if (GatheringT > 3)
            {
                isTouch = false;
                GatheringT = 0;
            }

        }
    }

    private void LateUpdate()
    {
        rightHandTracking.HandTracking(rightAttach);
    }
}
