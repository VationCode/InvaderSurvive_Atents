//**********23.01.01 : 인벤토리칸에 해당하는 기능, 필드아이템으로부터 받아올 정보들을 담을 변수와 마우스좌표가 들어왔는지 처리 - 허인호 
//**********InventorySlot : 인벤토리 칸 클래스
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
