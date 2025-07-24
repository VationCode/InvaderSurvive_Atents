using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region SingleTon
    public static WeaponManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    #endregion

    public enum EWeapon
    {
        Sword,
        Gun
    }

    private string weaponParentNode = "-----Model-----/Player/Weapon/WeaponParent";
    private const string gunPosNode = "-----Model-----/Player/Character/Vanguard By T. Choonyung/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/GunRightAttach";
    private const string swordPosNode = "-----Model-----/Player/Character/Vanguard By T. Choonyung/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/SwordRightAttach";
    private GameObject gunRightAttachObj; // 무기 프리팹의 경우 자식 제외하고는 public으로 저장해놓을수가 없기에 경로로 검색해서 넣어줌 Find조금이라도 빠르게 하기위해서
    private GameObject swordRightAttachObj;

   

    private GameObject WeaponParentObj;


    public List<GameObject> gunList;
    public List<GameObject> swordList;

    
    private GameObject currentWeaponObj;
    private RightHandTracking rightHandTracking = new RightHandTracking();

    void Start()
    {
        WeaponParentObj = GameObject.Find(weaponParentNode);

        //gunList = new List<GameObject>();
        ResourceManager.Instance.LoadrcGunItem();
        gunRightAttachObj = GameObject.Find(gunPosNode);
        foreach (GameObject one in ResourceManager.Instance.rcGunItemModelList)
        {
         
            gunList.Add(one);
        }

        //swordList = new List<GameObject>();
        ResourceManager.Instance.LoadrcSwordItem();
        swordRightAttachObj = GameObject.Find(swordPosNode);
        foreach (GameObject one in ResourceManager.Instance.rcSwordItemModelList)
        {
            swordList.Add(one);
        }

        for (int i = 0; i < gunList.Count; i++)
        {
            if(gunList[i].name == "SciFiGunLightWhite")
            {
                currentWeaponObj = gunList[i];
            }
        }
        currentWeaponObj = Instantiate(currentWeaponObj);
        SetParent(currentWeaponObj, gunRightAttachObj);
        Debug.Log("Gun Count = "+ gunList.Count + "  " + " Sword Count  =" + swordList.Count);
    }

    public GameObject SwordCreateInstance(string _name)
    {
        GameObject enableObj = EnableSwordObject(_name);
        if (enableObj != null)
        {
            SetParent(enableObj, swordRightAttachObj);
            return enableObj;

        }
        else
        {
            GameObject swordObj = swordList.Find(o => o.name.Equals(_name));
            SetParent(swordObj, swordRightAttachObj);
            return Instantiate<GameObject>(swordObj, WeaponParentObj.transform);
        }
    }
    public GameObject GunCreateInstance(string _name)
    {
        GameObject enableObj = EnableGunObject(_name);
        if (enableObj != null)
        {
            SetParent(enableObj, gunRightAttachObj);
            return enableObj;
        }
        else
        {
            GameObject gunObj = gunList.Find(o => o.name.Equals(_name));
            SetParent(gunObj, gunRightAttachObj);
            return Instantiate<GameObject>(gunObj, WeaponParentObj.transform);
        }
    }

    public GameObject EnableGunObject(string _name)
    {
        GameObject gun = gunList.Find(o => o.gameObject.activeSelf == false && (o.gameObject.name.Equals(_name)));
        if (gun != null) return gun.gameObject;
        return null;
    }
    public GameObject EnableSwordObject(string _name)
    {
        GameObject sword = swordList.Find(o => o.gameObject.activeSelf == false && (o.gameObject.name.Equals(_name)));
        if (sword != null) return sword.gameObject;
        return null;
    }
    public void DestoryObj()
    {
        GameObject.Destroy(currentWeaponObj);
    }

    public void SetParent(GameObject _weapon, GameObject _rightAttach)
    {
        _weapon.transform.SetParent(WeaponParentObj.transform);
        rightHandTracking = _weapon.GetComponent<RightHandTracking>();
        rightHandTracking.HandTracking(_rightAttach);
    }


    void Update()
    {
       
    }

    private void LateUpdate()
    {
        rightHandTracking.HandTracking(gunRightAttachObj);
        /*if (rightHandTracking == null || currentWeapon == null) return;
        if (eWeaponState == EWeaponState.Sword)
        {
            rightHandTracking.HandTracking(swordRightAttach.transform);
        }
        else if (eWeaponState == EWeaponState.Gun)
        {
            rightHandTracking.HandTracking(gunRightAttach.transform);
        }*/
    }


}
