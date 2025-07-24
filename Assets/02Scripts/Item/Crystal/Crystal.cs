using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField]
    private int hp; //크리스탈의 체력

    [SerializeField]
    private float destoryTime; //파편 사라지는 시간 (파편 효과는 크리스탈 조각들에게 각각 리지드바디를 삽입하고 메스, 드래그, 앵글값을 이용하여 튀게만듬)

    [SerializeField]
    private BoxCollider col;

    //필요한 게임 오브젝트
    //일반 크리스탈 (두개의 크리스탈(일반, 파면)오브젝트를 활성화 비활성화로 제어)
    [SerializeField]
    private GameObject go_Crystal; 
    [SerializeField]
    private GameObject go_Debris; //깨진 크리스탈
    [SerializeField]
    private GameObject go_RuneCrystalItemPrefab;
    [SerializeField]
    private int runeCount; //나타날 룬아이템 갯수

    //필요한 사운드 이름
    [SerializeField]
    private string CrystalBreakSound;

   
    public void Mining() //채굴
    {
        hp--;
        if (hp <= 0)
        {
            Destruction();
        }
    }
    private void Destruction()
    {
        for (int i = 0; i < runeCount; i++)
        {
            Instantiate(go_RuneCrystalItemPrefab, go_Crystal.transform.position, Quaternion.identity);
        }
        //SoundManager.Instance.PlaySE(CrystalBreakSound);
        col.enabled = false;
        Destroy(go_Crystal); //기존 일반 크리스탈은 제거

        go_Debris.SetActive(true);
        Destroy(go_Debris, destoryTime);
    }
}
