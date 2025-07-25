//**********23.01.11 : 적 공격 시 피 효과 이펙트 리소스 추가
//**********22.12.10 : 리소스 모델들 불러오기
//**********ResourceManager : 리소스 매니저 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region SingleTon
    public static ResourceManager Instance;
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

    //Model
    public List<GameObject> rcEquipmentItemModelList { get; private set; }
    public List<GameObject> rcIngredientItemModelList { get; private set; }
    public List<GameObject> rcToolItemModelList { get; private set; }
    public List<GameObject> rcUseItemModelList { get; private set; }
    public List<GameObject> rcGunItemModelList { get; private set; }
    public List<GameObject> rcSwordItemModelList { get; private set; }
    public List<GameObject> rcMonsterModelList { get; private set; }
    public List<GameObject> rcNPCModelList { get; private set; }
    public List<GameObject> rcPlayerModelList { get; private set; }
    public List<GameObject> rcMonsterBulletModelList { get; private set; }
    private string rcEquipmentItemFolder = "01rcModels/Item/Equipments/Prefab";
    private string rcIngredientItemFolder = "01rcModels/Item/Ingredients/Prefab";
    private string rcToolItemFolder = "01rcModels/Item/Tools/Prefab";
    private string rcUseItemFolder = "01rcModels/Item/UseItems/Prefab";
    private string rcGunItemFolder = "01rcModels/Item/Weapons/Gun/Prefab";
    private string rcSwordItemFolder = "01rcModels/Item/Weapons/Sword/Prefab";
    private string rcMonsterFolder = "01rcModels/Monster/Prefab";
    private string rcNPCFolder = "01rcModels/NPC/Prefab";
    private string rcPlayerFolder = "01rcModels/Player/Prefab";
    private string rcMonsterBulletFolder = "01rcModels/Monster/Bullet/Prefab";
    //Texture
    public List<GameObject> rcTextureList { get; private set; }
    private string rcTextureFolder = "02rcTextures/_Prefab";
    //Plarticle
    public List<GameObject> rcDamageEffectlList { get; private set; }
    public List<GameObject> rcParticleList { get; private set; }
    private string rcDamageEffectFolder = "03rcParticles/DamageEffect/_Prefab";
    private string rcParticleFolder = "03rcParticles/_Prefab";

    //Model
    /*public void LoadrcEquipmentItem()
    {
        rcEquipmentItemModelList = new List<GameObject>();
        GameObject[] EquipmentItemModelFold = Resources.LoadAll<GameObject>(rcEquipmentItemFolder);
        foreach (GameObject one in EquipmentItemModelFold)
        {
            rcEquipmentItemModelList.Add(one);
        }
    }
    public void LoadrcIngredientItem()
    {
        rcIngredientItemModelList = new List<GameObject>();
        GameObject[] rcIngredientItemModellFold = Resources.LoadAll<GameObject>(rcIngredientItemFolder);
        foreach (GameObject one in rcIngredientItemModellFold)
        {
            rcIngredientItemModelList.Add(one);
        }
    }
    public void LoadrcToolItem()
    {
        rcToolItemModelList = new List<GameObject>();
        GameObject[] rcToolItemModelFold = Resources.LoadAll<GameObject>(rcToolItemFolder);
        foreach (GameObject one in rcToolItemModelFold)
        {
            rcToolItemModelList.Add(one);
        }
    }
    public void LoadrcUseItem()
    {
        rcUseItemModelList = new List<GameObject>();
        GameObject[] rcUseItemModelFold = Resources.LoadAll<GameObject>(rcUseItemFolder);
        foreach (GameObject one in rcUseItemModelFold)
        {
            rcUseItemModelList.Add(one);
        }
    }
    public void LoadrcSwordItem()
    {
        rcSwordItemModelList = new List<GameObject>();
        GameObject[] rcSwordItemModelFold = Resources.LoadAll<GameObject>(rcSwordItemFolder);
      
        foreach (GameObject one in rcSwordItemModelFold)
        {
            rcSwordItemModelList.Add(one);
        }
    }
    public void LoadrcGunItem()
    {
        rcGunItemModelList = new List<GameObject>();
        GameObject[] rcGunItemModelFold = Resources.LoadAll<GameObject>(rcGunItemFolder);

        foreach (GameObject one in rcGunItemModelFold)
        {
            rcGunItemModelList.Add(one);
        }
    }
    public void LoadrcMonster()
    {
        rcMonsterModelList = new List<GameObject>();
        GameObject[] rcMonsterModelFold = Resources.LoadAll<GameObject>(rcMonsterFolder);
        foreach (GameObject one in rcMonsterModelFold)
        {
            rcMonsterModelList.Add(one);
        }
    }

    public void LoadrcNPC()
    {
        rcNPCModelList = new List<GameObject>();
        GameObject[] rcNPCModelFold = Resources.LoadAll<GameObject>(rcNPCFolder);
        foreach (GameObject one in rcNPCModelFold)
        {
            rcNPCModelList.Add(one);
        }
    }
    public void LoadrcPlayer()
    {
        rcPlayerModelList = new List<GameObject>();
        GameObject[] rcPlayerModelFold = Resources.LoadAll<GameObject>(rcPlayerFolder);

        foreach (GameObject one in rcPlayerModelFold)
        {
            rcPlayerModelList.Add(one);
        }
    }

    public void LoadrcMonsterBullet()
    {
        rcMonsterBulletModelList = new List<GameObject>();
        GameObject[] rcMonsterBulletFold = Resources.LoadAll<GameObject>(rcMonsterBulletFolder);

        foreach (GameObject one in rcMonsterBulletFold)
        {
            rcMonsterBulletModelList.Add(one);
        }
    }

    public GameObject GetrcEquipmentItem(string _name)
    {
        return rcEquipmentItemModelList.Find(o => (o.name.Equals(_name)));
    }
    public GameObject GetrcIngredientItem(string _name)
    {
        return rcIngredientItemModelList.Find(o => (o.name.Equals(_name)));
    }

    public GameObject GetrcToolItem(string _name)
    {
        return rcToolItemModelList.Find(o => (o.name.Equals(_name)));
    }
    public GameObject GetrcUseItem(string _name)
    {
        return rcUseItemModelList.Find(o => (o.name.Equals(_name)));
    }
    public GameObject GetrcGunItem(string _name)
    {
        return rcGunItemModelList.Find(o => (o.name.Equals(_name)));
    }
    public GameObject GetrcSwordItem(string _name)
    {
        return rcSwordItemModelList.Find(o => (o.name.Equals(_name)));
    }
    public GameObject GetrcMonster(string _name)
    {
        return rcMonsterModelList.Find(o => (o.name.Equals(_name)));
    }
    public GameObject GetrcNPC(string _name)
    {
        return rcNPCModelList.Find(o => (o.name.Equals(_name)));
    }
    public GameObject GetrcPlayer(string _name)
    {
        return rcPlayerModelList.Find(o => (o.name.Equals(_name)));
    }
    public GameObject GetrcMonsterBullet(string _name)
    {
        return rcMonsterBulletModelList.Find(o => (o.name.Equals(_name)));
    }
    //Texture
    public void LoadrcTexture()
    {
        rcTextureList = new List<GameObject>();
        GameObject[] TextureFolders = Resources.LoadAll<GameObject>(rcTextureFolder);
        foreach (GameObject one in TextureFolders)
        {
            rcTextureList.Add(one);
        }
    }

    public GameObject GetrcTexture(string _name)
    {
        return rcTextureList.Find(o => (o.name.Equals(_name)));
    }
    //particle
    public void LoadrcDamageEffect()
    {
        rcDamageEffectlList = new List<GameObject>();
        GameObject[] damageEffectFolders = Resources.LoadAll<GameObject>(rcDamageEffectFolder);
        foreach (GameObject one in damageEffectFolders)
        {
            rcDamageEffectlList.Add(one);
        }
    }
    public void LoadrcParticle()
    {
        rcParticleList = new List<GameObject>();
        GameObject[] particleFolders = Resources.LoadAll<GameObject>(rcParticleFolder);
        foreach (GameObject one in particleFolders)
        {
            rcParticleList.Add(one);
        }
    }

    public GameObject GetrcDamageEffect(string _name)
    {
        return rcDamageEffectlList.Find(o => (o.name.Equals(_name)));
    }

    public GameObject GetrcParticle(string _name)
    {
        return rcParticleList.Find(o => (o.name.Equals(_name)));
    }*/


}
