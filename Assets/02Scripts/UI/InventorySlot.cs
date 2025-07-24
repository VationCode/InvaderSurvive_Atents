//**********23.01.01 : �κ��丮ĭ�� �ش��ϴ� ���, �ʵ���������κ��� �޾ƿ� �������� ���� ������ ���콺��ǥ�� ���Դ��� ó�� - ����ȣ 
//**********InventorySlot : �κ��丮 ĭ Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum EType
{
    InventorySlot,
    useSlot,
}
public class InventorySlot : MonoBehaviour
{
    public EType slotType = EType.InventorySlot;
    public Image image;
    public int itemCount;
    public TextMeshProUGUI tmp;

    public ItemInfo itemInfo;

    float xMin;
    public float XMIN
    {
        get
        {
            xMin = transform.position.x - image.rectTransform.rect.width * 0.5f;
            return xMin;
        }
    }
    float xMax;
    public float XMAX
    {
        get
        {
            xMax = transform.position.x + image.rectTransform.rect.width * 0.5f;
            return xMax;
        }
    }

    float yMin;
    public float YMIN
    {
        get
        {
            yMin = transform.position.y - image.rectTransform.rect.height * 0.5f;
            return yMin;
        }
    }
    float yMax;
    public float YMAX
    {
        get
        {
            yMax = transform.position.y + image.rectTransform.rect.height * 0.5f;
            return yMax;
        }
    }

    void Start()
    {
        itemCount = 0;
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        image = transform.GetChild(0).gameObject.GetComponent<Image>();
        image.gameObject.SetActive(false);
    }

    public bool IsInRect(Vector2 pos)
    {
        if(pos.x >= XMIN && pos.x <= XMAX && pos.y >= YMIN && pos.y <= YMAX)
        {
            return true;
        }
        return false;
    }

}
