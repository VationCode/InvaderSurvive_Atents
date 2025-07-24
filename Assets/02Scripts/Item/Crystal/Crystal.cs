using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField]
    private int hp; //ũ����Ż�� ü��

    [SerializeField]
    private float destoryTime; //���� ������� �ð� (���� ȿ���� ũ����Ż �����鿡�� ���� ������ٵ� �����ϰ� �޽�, �巡��, �ޱ۰��� �̿��Ͽ� Ƣ�Ը���)

    [SerializeField]
    private BoxCollider col;

    //�ʿ��� ���� ������Ʈ
    //�Ϲ� ũ����Ż (�ΰ��� ũ����Ż(�Ϲ�, �ĸ�)������Ʈ�� Ȱ��ȭ ��Ȱ��ȭ�� ����)
    [SerializeField]
    private GameObject go_Crystal; 
    [SerializeField]
    private GameObject go_Debris; //���� ũ����Ż
    [SerializeField]
    private GameObject go_RuneCrystalItemPrefab;
    [SerializeField]
    private int runeCount; //��Ÿ�� ������� ����

    //�ʿ��� ���� �̸�
    [SerializeField]
    private string CrystalBreakSound;

   
    public void Mining() //ä��
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
        Destroy(go_Crystal); //���� �Ϲ� ũ����Ż�� ����

        go_Debris.SetActive(true);
        Destroy(go_Debris, destoryTime);
    }
}
