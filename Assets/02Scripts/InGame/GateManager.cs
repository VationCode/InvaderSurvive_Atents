using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GateManager : MonoBehaviour
{
    #region SingleTon
    public static GateManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Collider playerColl;
    public Collider[] Coll;
    private Collider currentColl;
    private Collider prevColl;
    private List<string> enumName;
    private void Start()
    {
        currentColl = Coll[0];
        //UIManager.Instance.FadeInOut(currentColl.name);
        //SoundManager.Instance.PlaySE(currentColl.name);
    }

    public void GateEffect()
    {
        int count = 0;
        for (int i = 0; i < GateManager.Instance.Coll.Length; i++)
        {
            if (playerColl.bounds.Intersects(GateManager.Instance.Coll[i].bounds))
            {
                count++;
                currentColl = GateManager.Instance.Coll[i];
            }
        }
        if (count <= 1)
        {
            if (currentColl != prevColl)
            {
                prevColl = currentColl;
                UIManager.Instance.FadeInOut(prevColl.name);
                SoundManager.Instance.PlaySE(prevColl.name);
            }
        }
    }

    private void Update()
    {
        GateEffect();
    }

}
