//**********23.01.03 : �κ��丮 �ߺ�Ȯ�� ���� �߻� ��ġ���� - ����ȣ
//**********23.01.02 : ��뽽�� �� ������ ������ ��� �Ϸ��Ͽ����� ������ ���� ������ ��� ���� ���۾��� - ����ȣ
//**********23.01.02 :��ũ�Ѻ� ó���� OnPointerDown���� �巡�� �� �ٷ� Up�� ȣ���ع����� ���� �߻� ��ũ�Ѻ��� �̹����� ���� - ����ȣ
//**********23.01.01 : �ʵ���������κ��� �޼����� �ޱ�, �κ��丮 ���Կ� �������� ����, ������ �̹��� �̵� ó��(������) - ����ȣ 
//**********Inventory : �κ��丮 Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, ITriggerItem, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region SingleTon
    public static Inventory Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        addSlotBtn.onClick.AddListener(AddSlot);
        openInventoryBtn.onClick.AddListener(OpenInventory);
        closeBtn.onClick.AddListener(CloseInventory);

        SetDragThreshold();
    }
    #endregion
    #region Touch�ΰ���
    //��ũ�Ѻ� ��ġ�� �ΰ��� ������ ����(��ũ�Ѻ��� �巡�� �κа� ��ư�̳� �̹������� �浹�� ��ư�� �� �ȴ����� ��Ȳ�� ����)
    private void SetDragThreshold()
    {
        if (eventSystem != null)
        {
            eventSystem.pixelDragThreshold = (int)(dragThresholdCM * Screen.dpi / inchToCm);
        }
    }
    private const float inchToCm = 2.54f;
    [SerializeField] private EventSystem eventSystem = null;
    [SerializeField] private float dragThresholdCM = 0.5f;
    #endregion

    delegate void DoUseItem();

    [SerializeField] private Button addSlotBtn;
    [SerializeField] private Button openInventoryBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private GameObject Panel; //�κ� �Ѱ� �����ϱ�����
    Image image;
    //private ScrollRect scrollRect;
    //private Image viewport; //�����ɽ�Ʈ Ÿ�� �����Ϸ���

    public List<InventorySlot> inventorySlotList;
    private int[] useSlotNumArray;
    DragSlot dragSlot;

    public TextMeshProUGUI goldTMP;
    public int gold;
    public int addSlotGold;

    public bool isInventoryActive { get; private set; } //1. On/Off����κ��丮 2. �������� ��� �ٸ������� ��� ��� ���ؼ��� ���
    bool isEmptySlotClick; //��ĭ Ŭ���� ���� ���� ���� �����ϱ�����

    ParticleSystem particle;
    void Start()
    {
        ResourceManager.Instance.LoadrcParticle();
        dragSlot = GetComponentInChildren<DragSlot>();
        dragSlot.gameObject.SetActive(false);
        image = GetComponent<Image>();
        Panel = transform.GetChild(0).gameObject;
        useSlotNumArray = new int[2];


        gold = 5000;
        addSlotGold = 1000;
        goldTMP.text = gold.ToString();

        inventorySlotList = new List<InventorySlot>();
        InventorySlot[] slot = GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot one in slot)
        {
            inventorySlotList.Add(one);
        }

        int num = 0;
        for (int i = 0; i < inventorySlotList.Count; i++)
        {
            if (inventorySlotList[i].slotType == EType.useSlot)
            {
                useSlotNumArray[num] = i;
                num++;
            }
        }
    }

    //��ũ�Ѻ� ������ ���Ƴ���
    void AddSlot()
    {
        /*if (gold - addSlotGold >= 0)
        {
            gold -= addSlotGold;
            goldTMP.text = gold.ToString();
            InventorySlot rcSlot = Resources.Load<InventorySlot>("02rcTextures/UI/Inventory/_Prefab/InventorySlot");
            GameObject newSlot = GameObject.Instantiate<GameObject>(rcSlot.gameObject);
            newSlot.transform.SetParent(scrollRect.content);
            inventorySlots.Add(rcSlot);
        }*/
    }

    void OpenInventory()
    {
        isInventoryActive = true;
    }
    void CloseInventory()
    {
        isInventoryActive = false;
    }

    //�κ��丮 On/Off
    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryActive = !isInventoryActive;
        }
        Panel.SetActive(isInventoryActive);
        if (isInventoryActive)
        {
            image.raycastTarget = true;
        }
        else image.raycastTarget = false;
    }

    void UseItem()
    {
        if (inventorySlotList[useSlotNumArray[0]].image.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                /*if(inventorySlots) //�������� ���
                {
                    doUseItem = 
                }
                else if() //���������� ���
                {

                }*/
                UsedItemSlot(useSlotNumArray[0]);
            }
        }
        if (inventorySlotList[useSlotNumArray[1]].image.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                /*if()//�������� ��� 
                {

                }
                else if()//���������� ���
                {

                }*/
                UsedItemSlot(useSlotNumArray[1]);
            }
        }
    }

    void UsedItemSlot(int num)
    {
        inventorySlotList[num].itemCount -= 1;
        inventorySlotList[num].tmp.text = inventorySlotList[num].itemCount.ToString();
        if (inventorySlotList[num].itemCount <= 0) inventorySlotList[num].image.gameObject.SetActive(false);
        if(inventorySlotList[num].itemInfo.eUseItemType == EUseItemType.HealPotion)
        {
            GameObject obj = ResourceManager.Instance.GetrcParticle("HealthPotion");
            obj = Instantiate(obj, PlayerMovementManager.Instance.target.transform.position, PlayerMovementManager.Instance.playerObj.transform.rotation, PlayerMovementManager.Instance.playerObj.transform);
            if(PlayerMovementManager.Instance.playerInfo.hp <= 100)
            {
                PlayerMovementManager.Instance.playerInfo.hp += 30;
            }
            Destroy(obj, 1f);
        }
        else if (inventorySlotList[num].itemInfo.eUseItemType == EUseItemType.ManaPotion)
        {
            GameObject obj = ResourceManager.Instance.GetrcParticle("ManaPotion");
            obj = Instantiate(obj, PlayerMovementManager.Instance.target.transform.position, PlayerMovementManager.Instance.playerObj.transform.rotation, PlayerMovementManager.Instance.playerObj.transform);
            if(PlayerMovementManager.Instance.playerInfo.mana <= 100)
            {
                PlayerMovementManager.Instance.playerInfo.mana += 30;
            }
            Destroy(obj, 1f);
        }
    }

    void Update()
    {
        TryOpenInventory();
        UseItem();
        if (!isInventoryActive) dragSlot.gameObject.SetActive(false);
    }
    //���� ���� ������������ �κ��丮�� ����
    public virtual void ApplyTriggerItemInfo(ItemInfoMessage itemInfoMessage)
    {
        if (itemInfoMessage.itemInfo.itmeName != string.Empty) //�������̽��� ���� �޾ƿ�
        {

            for (int i = 0; i < inventorySlotList.Count; i++)
            {
                if (!inventorySlotList[i].image.gameObject.activeSelf) continue;

                if (itemInfoMessage.itemInfo.itmeName == inventorySlotList[i].itemInfo.itmeName) //�ߺ� üũ
                {
                    inventorySlotList[i].itemCount += 1;
                    inventorySlotList[i].tmp.text = inventorySlotList[i].itemCount.ToString();
                    return;
                }
            }

            for (int i = 0; i < inventorySlotList.Count; i++)
            {
                if (!inventorySlotList[i].image.gameObject.activeSelf)
                {
                    inventorySlotList[i].image.gameObject.SetActive(true);
                    inventorySlotList[i].image.sprite = itemInfoMessage.itemInfo.sprite;
                    inventorySlotList[i].itemCount += 1;
                    inventorySlotList[i].tmp.text = inventorySlotList[i].itemCount.ToString();
                    inventorySlotList[i].itemInfo = itemInfoMessage.itemInfo;
                    break;
                }
            }

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < inventorySlotList.Count; i++)
        {
            if (inventorySlotList[i].IsInRect(eventData.position))
            {
                if (inventorySlotList[i].image.gameObject.activeSelf == true)
                {
                    isEmptySlotClick = false;
                    dragSlot.transform.position = eventData.position;
                    dragSlot.gameObject.SetActive(true);
                    dragSlot.image.sprite = inventorySlotList[i].image.sprite;
                    dragSlot.itemCount = inventorySlotList[i].itemCount;
                    dragSlot.prevSlotNum = i;
                    dragSlot.itemInfo = inventorySlotList[i].itemInfo;
                    break;
                }

                else if (inventorySlotList[i].image.gameObject.activeSelf == false)
                {
                    isEmptySlotClick = true;
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        //dragSlot.image.sprite = null;
        dragSlot.gameObject.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragSlot.transform.position = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        dragSlot.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isEmptySlotClick) return;
        for (int i = 0; i < inventorySlotList.Count; i++)
        {
            if (inventorySlotList[i].IsInRect(eventData.position))
            {
                if (inventorySlotList[i].image.gameObject.activeSelf == false) //������ϰ� ��ü
                {
                    inventorySlotList[i].image.gameObject.SetActive(true);
                    inventorySlotList[i].image.sprite = dragSlot.image.sprite;
                    inventorySlotList[i].itemCount = dragSlot.itemCount;
                    inventorySlotList[i].tmp.text = dragSlot.itemCount.ToString();
                    inventorySlotList[i].itemInfo = dragSlot.itemInfo;

                    inventorySlotList[dragSlot.prevSlotNum].itemCount = 0;
                    inventorySlotList[dragSlot.prevSlotNum].tmp.text = inventorySlotList[dragSlot.prevSlotNum].itemCount.ToString();
                    inventorySlotList[dragSlot.prevSlotNum].image.gameObject.SetActive(false);

                    dragSlot.transform.position = eventData.position;
                    dragSlot.image.sprite = null;
                    dragSlot.gameObject.SetActive(false);
                }
                else //�������� �ִ� �����ϰ� ��ü
                {
                    //���� ����
                    inventorySlotList[dragSlot.prevSlotNum].image.sprite = inventorySlotList[i].image.sprite;
                    inventorySlotList[dragSlot.prevSlotNum].itemCount = inventorySlotList[i].itemCount;
                    inventorySlotList[dragSlot.prevSlotNum].tmp.text = inventorySlotList[i].itemCount.ToString();
                    inventorySlotList[dragSlot.prevSlotNum].itemInfo = inventorySlotList[i].itemInfo;

                    //��ü ����
                    inventorySlotList[i].image.sprite = dragSlot.image.sprite;
                    inventorySlotList[i].itemCount = dragSlot.itemCount;
                    inventorySlotList[i].tmp.text = dragSlot.itemCount.ToString();
                    inventorySlotList[i].itemInfo = dragSlot.itemInfo;

                    dragSlot.transform.position = eventData.position;
                    dragSlot.image.sprite = null;
                    dragSlot.itemCount = 0;
                    dragSlot.prevSlotNum = i;
                    dragSlot.gameObject.SetActive(false);
                }
            }
        }
    }
}
