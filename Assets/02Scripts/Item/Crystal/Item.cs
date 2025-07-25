//**********23.01.03 : �ʵ������ �ٷ�� Ŭ�������� ������ Ŭ������ �缳��
//**********23.01.02 : �ʵ� �����۰� �κ��丮���� �����̳� ��� �� �����ۿ� ���� ���� �ʿ�
//**********22.12.30 : �ʵ忡 ������ �������� ĳ���� �ٿ������ ������ ���� ����ó���� �κ��丮�� �޼��� ���� ����
//**********Item : ������ Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemInfo itemInfo;

    // �ݶ��̴� �ٿ�� ���� �÷��̾���� ���� Ȯ��
    private Collider coll;
    private Collider playerColl; 

    private void Start()
    {
        coll = GetComponentInChildren<Collider>();
        playerColl = PlayerMovementManager.Instance.playerObj.GetComponent<Collider>();
        //itemInfo.sprite = Resources.Load<Sprite>("02rcTextures/UI/Item/" + itemInfo.itmeName);
        //itemInfo.sprite = m_itemSprite;
    }

    //�ʵ忡�� �÷��̾�� ���� �� ���� ���� �Լ�
    private void TriggerEnter()
    {
        if (playerColl != null && coll.bounds.Intersects(playerColl.bounds)) //�÷��̾�� �ٿ��� ������ ���� ��
        {
            var iTriggerItem = Inventory.Instance.gameObject.GetComponent<ITriggerItem>(); //�κ��丮 ��ũ��Ʈ�� ��ӵǾ��ִ� �������̽�
            ItemInfoMessage itemInfoMessage; //����ü�� ������ ��� ITriggerItem �������̽��� �Լ��� �κ��丮�� ����Ͽ� ��������

            itemInfoMessage.itemInfo = itemInfo;
            Destroy(this.gameObject);
            iTriggerItem.ApplyTriggerItemInfo(itemInfoMessage); //ITriggerItem �������̽��� ApplyTriggerItemInfo�Լ��� �κ��丮�� ������ ���� ����
        }
    }

    private void Update()
    {
        TriggerEnter();
    }
}
