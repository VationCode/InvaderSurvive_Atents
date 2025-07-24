//**********23.01.01 : 인벤토리에서 아이템 이동 시 아이템의 정보를 보관 및 이미지 이동 담당- 허인호 
//**********DragSlot : 인벤토리 이동 이미지 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot:MonoBehaviour
{
    [HideInInspector] public Image image;
    [HideInInspector] public int itemCount;
    [HideInInspector] public int prevSlotNum;
    [HideInInspector] public ItemInfo itemInfo;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
}
