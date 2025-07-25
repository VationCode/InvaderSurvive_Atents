//**********23.01.03 : 필드아이템 다루는 클래스에서 아이템 클래스로 재설계
//**********23.01.02 : 필드 아이템과 인벤토리에서 장착이나 사용 시 아이템에 대한 구분 필요
//**********22.12.30 : 필드에 생성된 아이템을 캐릭터 바운즈와의 접촉을 통한 정보처리와 인벤토리로 메세지 정보 전달
//**********Item : 아이템 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemInfo itemInfo;

    // 콜라이더 바운즈를 통한 플레이어와의 접촉 확인
    private Collider coll;
    private Collider playerColl; 

    private void Start()
    {
        coll = GetComponentInChildren<Collider>();
        playerColl = PlayerMovementManager.Instance.playerObj.GetComponent<Collider>();
        //itemInfo.sprite = Resources.Load<Sprite>("02rcTextures/UI/Item/" + itemInfo.itmeName);
        //itemInfo.sprite = m_itemSprite;
    }

    //필드에서 플레이어와 접촉 시 정보 전달 함수
    private void TriggerEnter()
    {
        if (playerColl != null && coll.bounds.Intersects(playerColl.bounds)) //플레이어와 바운즈 접촉이 있을 시
        {
            var iTriggerItem = Inventory.Instance.gameObject.GetComponent<ITriggerItem>(); //인벤토리 스크립트에 상속되어있는 인터페이스
            ItemInfoMessage itemInfoMessage; //구조체에 정보를 담고 ITriggerItem 인터페이스의 함수로 인벤토리에 상속하여 정보전달

            itemInfoMessage.itemInfo = itemInfo;
            Destroy(this.gameObject);
            iTriggerItem.ApplyTriggerItemInfo(itemInfoMessage); //ITriggerItem 인터페이스의 ApplyTriggerItemInfo함수로 인벤토리에 아이템 정보 전달
        }
    }

    private void Update()
    {
        TriggerEnter();
    }
}
