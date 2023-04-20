using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

//背包的单个槽
public class UIInventorySlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private Canvas parentCancas;
    private Camera mainCamera;
    private Transform parentItemTransform; //所有物品的父物体Trasnform，即场景中的Items
    private GameObject draggedItem; //拖动物体时产生的临时UI

    //=================================用于获取UIInventorySlot的三个子物体（在编辑器填充）==================================
    public Image inventorySlotImage;
    public Image inventorySlotHighlight; //选中高光
    public TextMeshProUGUI textMeshProUGUI;
    //====================================================================================================================

    public ItemDetails itemDetails;
    public int itemQuantity;

    [HideInInspector] public bool isSelected = false; //物品是否被选中
    [SerializeField] private GameObject inventoryTextBoxPrefab;
    [SerializeField] private UIInventoryBar inventoryBar;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int slotNumber; //当前物品槽序号

    private void Awake()
    {
        parentCancas = GetComponentInParent<Canvas>();
    }
    private void Start()
    {
        mainCamera = Camera.main;
        parentItemTransform = GameObject.FindGameObjectWithTag(Tags.ItemsParentTransform).transform;
    }

    //在拖动之前调用
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (null != itemDetails)
        {
            Player.Instance.DisablePlayerInputAndResetMovement();

            draggedItem = Instantiate(inventoryBar.inventoryBarDraggedItem, inventoryBar.transform);

            Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = inventorySlotImage.sprite;

            SetSelectedItem();
        }
    }

    //拖动中调用
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (null != draggedItem)
        {
            draggedItem.transform.position = Input.mousePosition;
            //Debug.Log(draggedItem.transform.position);
        }
    }

    //拖动结束后调用
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (null != draggedItem)
        {
            Destroy(draggedItem);

            //如果鼠标指针最后停留在背包槽
            if (null != eventData.pointerCurrentRaycast.gameObject && null != eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>())
            {
                //获取鼠标停留的物品槽序号
                int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>().slotNumber;
                //改变数据队列排序
                InventoryManager.Instance.SwapInventoryItems(InventoryLocation.player, slotNumber, toSlotNumber);

                DestroyInventoryTextBox();

                ClearSelectedItem();

                ClearSelectedItem();
            }
            //指针不停留在背包槽
            else if (itemDetails.canBeDropped)
            {
                DropSelectedItemAtMousePosition();
            }
        }
        //允许玩家移动
        Player.Instance.PlayerInputIsDisabled = false;
    }

    //鼠标停留时调用
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //用物品详细信息填充文字描述框
        if (0 != itemQuantity)
        {
            inventoryBar.inventoryTextBoxGameobject = Instantiate(inventoryTextBoxPrefab, transform.position, Quaternion.identity);
            inventoryBar.inventoryTextBoxGameobject.transform.SetParent(parentCancas.transform, false);

            UIInventoryTextBox inventoryTextBox = inventoryBar.inventoryTextBoxGameobject.GetComponent<UIInventoryTextBox>();

            string itemTypeDescription = InventoryManager.Instance.GetItemDescription(itemDetails.itemType);

            //填充描述框
            inventoryTextBox.SetTextboxText(itemDetails.itemDescription, itemTypeDescription, "", itemDetails.itemLongDescription, "", "");

            //根据物品栏位置调整描述框位置
            if (inventoryBar.IsInventoryBarPositionBottom)
            {
                inventoryBar.inventoryTextBoxGameobject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
                inventoryBar.inventoryTextBoxGameobject.transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
            }
            else
            {
                inventoryBar.inventoryTextBoxGameobject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                inventoryBar.inventoryTextBoxGameobject.transform.position = new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z);
            }
        }
    }

    //鼠标离开时调用
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        DestroyInventoryTextBox();
    }

    //鼠标点击时调用
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (PointerEventData.InputButton.Left == eventData.button)
        {
            if (true == isSelected)
            {
                ClearSelectedItem(); //取消选中
            }
            else
            {
                if (itemQuantity > 0)
                {
                    SetSelectedItem(); //选中
                }
            }
        }
    }

    //设置选中物品
    public void SetSelectedItem()
    {
        inventoryBar.ClearHighlightOnInventorySlots();
        isSelected = true;
        inventoryBar.SetHighlightedInventorySlots();
        InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.player, itemDetails.itemCode);

        if (itemDetails.canBeCarried)
        {
            Player.Instance.ShowCarriedItem(itemDetails.itemCode);
        }
        else
        {
            Player.Instance.ClearCarriedItem();
        }
    }

    //撤销选中
    public void ClearSelectedItem()
    {
        inventoryBar.ClearHighlightOnInventorySlots();
        isSelected = false;
        InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.player);

        Player.Instance.ClearCarriedItem();
    }

    //销毁描述框
    public void DestroyInventoryTextBox()
    {
        if (null != inventoryBar.inventoryTextBoxGameobject)
        {
            Destroy(inventoryBar.inventoryTextBoxGameobject);
        }
    }

    private void DropSelectedItemAtMousePosition()
    {
        if (null != itemDetails && isSelected)
        {
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            worldPosition.z = 0;

            GameObject itemGameobject = Instantiate(itemPrefab, worldPosition, Quaternion.identity, parentItemTransform);
            itemGameobject.GetComponentInChildren<SpriteRenderer>().sprite = itemDetails.itemSprite;

            Item item = itemGameobject.GetComponent<Item>();
            item.ItemCode = itemDetails.itemCode;
            

            //对0号Acorn的Sprite显示不全进行调试
            //Debug.Log((itemDetails.itemCode, itemDetails.itemDescription));
            //if (itemDetails.itemSprite) Debug.Log(itemDetails.itemSprite.name);
            //itemGameobject.GetComponentInChildren<Image>().sprite = itemDetails.itemSprite;

            InventoryManager.Instance.RemoveItem(InventoryLocation.player, item.ItemCode);

            //如果没有库存，清除选框高光
            if (-1 == InventoryManager.Instance.FindItemInInventory(InventoryLocation.player, item.ItemCode))
            {
                ClearSelectedItem();
            }
        }
    }
}

