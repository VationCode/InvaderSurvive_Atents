//**********23.01.01 : �κ��丮���� ������ �̵� �� �������� ������ ���� �� �̹��� �̵� ���- ����ȣ 
//**********DragSlot : �κ��丮 �̵� �̹��� Ŭ����
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
