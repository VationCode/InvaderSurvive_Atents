using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    #region SingleTon
    public static UseItem Instance;
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

    public void UsedHealPotion()
    {
        
    }
    public void UsedManaPotion()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
