//**********23.01.03 : 인벤토리 중복확인 문제 발생 고치는중 - 허인호
//**********23.01.02 : 사용슬롯 및 아이템 아이템 사용 완료하였으나 종류에 따른 아이템 사용 설계 재작업중 - 허인호
//**********23.01.02 :스크롤뷰 처음만 OnPointerDown에서 드래그 시 바로 Up를 호출해버리는 문제 발생 스크롤뷰대신 이미지로 변경 - 허인호
//**********23.01.01 : 필드아이템으로부터 메세지를 받기, 인벤토리 슬롯에 아이템을 저장, 아이템 이미지 이동 처리(제작중) - 허인호 
//**********Inventory : 인벤토리 클래스
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
    #region Touch민감도
    //스크롤뷰 터치시 민감도 조절을 위해(스크롤뷰의 드래그 부분과 버튼이나 이미지간의 충돌로 버튼이 잘 안눌리는 상황을 방지)
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
    [SerializeField] private GameObject Panel; //인벤 켜고 끄고하기위해
    Image image;
    //private ScrollRect scrollRect;
    //private Image viewport; //레이케스트 타겟 제어하려고

    public List<InventorySlot> inventorySlotList;
    private int[] useSlotNumArray;
    DragSlot dragSlot;

    public TextMeshProUGUI goldTMP;
    public int gold;
    public int addSlotGold;

    public bool isInventoryActive { get; private set; } //1. On/Off기능인벤토리 2. 켜져있을 경우 다른곳에서 기능 제어를 위해서도 사용
    bool isEmptySlotClick; //빈칸 클릭에 대한 문제 사전 차단하기위해

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

    //스크롤뷰 문제로 막아놓음
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

    //인벤토리 On/Off
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
                /*if(inventorySlots) //힐포션일 경우
                {
                    doUseItem = 
                }
                else if() //마나포션일 경우
                {

                }*/
                UsedItemSlot(useSlotNumArray[0]);
            }
        }
        if (inventorySlotList[useSlotNumArray[1]].image.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                /*if()//힐포션일 경우 
                {

                }
                else if()//마나포션일 경우
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
    //전달 받은 아이템정보를 인벤토리에 저장
    public virtual void ApplyTriggerItemInfo(ItemInfoMessage itemInfoMessage)
    {
        if (itemInfoMessage.itemInfo.itmeName != string.Empty) //인터페이스로 정보 받아옴
        {

            for (int i = 0; i < inventorySlotList.Count; i++)
            {
                if (!inventorySlotList[i].image.gameObject.activeSelf) continue;

                if (itemInfoMessage.itemInfo.itmeName == inventorySlotList[i].itemInfo.itmeName) //중복 체크
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
                if (inventorySlotList[i].image.gameObject.activeSelf == false) //빈공간하고 교체
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
                else //아이템이 있는 슬롯하고 교체
                {
                    //이전 슬롯
                    inventorySlotList[dragSlot.prevSlotNum].image.sprite = inventorySlotList[i].image.sprite;
                    inventorySlotList[dragSlot.prevSlotNum].itemCount = inventorySlotList[i].itemCount;
                    inventorySlotList[dragSlot.prevSlotNum].tmp.text = inventorySlotList[i].itemCount.ToString();
                    inventorySlotList[dragSlot.prevSlotNum].itemInfo = inventorySlotList[i].itemInfo;

                    //교체 슬롯
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
