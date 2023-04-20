using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryBar : MonoBehaviour
{
    [SerializeField] private Sprite blank16x16Sprite = null;
    [SerializeField] private UIInventorySlot[] inventorySlots = null;
    //在编辑器中填充的拖动实例
    public GameObject inventoryBarDraggedItem;
    //物品说明框
    [HideInInspector]public GameObject inventoryTextBoxGameobject;

    private RectTransform rectTransform;

    private bool _isInventoryBarPositionBottom = true;
    public bool IsInventoryBarPositionBottom { get => _isInventoryBarPositionBottom; set => _isInventoryBarPositionBottom = value; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        SwitchInventoryBarPosition();
    }

    private void OnEnable()
    {
        EventHandler.InventoryUpdateEvent += InventoryUpdated;
    }

    private void OnDisable()
    {
        EventHandler.InventoryUpdateEvent -= InventoryUpdated;
    }

    //背包初始化
    private void ClearInventorySlots()
    {
        if (inventorySlots.Length > 0)
        {
            for(int i=0;i< inventorySlots.Length; i++)
            {
                inventorySlots[i].inventorySlotImage.sprite = blank16x16Sprite;
                inventorySlots[i].textMeshProUGUI.text = "";
                inventorySlots[i].itemDetails = null;
                inventorySlots[i].itemQuantity = 0;
            }
        }
    }

    //更新背包
    private void InventoryUpdated(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (InventoryLocation.player == inventoryLocation)
        {
            ClearInventorySlots();

            if (inventorySlots.Length > 0 && inventoryList.Count > 0)
            {
                for(int i = 0; i < inventorySlots.Length; i++)
                {
                    //如果不满足条件循环跳出
                    if (i < inventoryList.Count)
                    {
                        int itemCode = inventoryList[i].itemCode;
                        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);

                        if (null != itemDetails)
                        {
                            inventorySlots[i].inventorySlotImage.sprite = itemDetails.itemSprite;
                            inventorySlots[i].textMeshProUGUI.text = inventoryList[i].itemQuantity.ToString();
                            inventorySlots[i].itemDetails = itemDetails;
                            inventorySlots[i].itemQuantity = inventoryList[i].itemQuantity;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    //改变背包UI屏幕显示位置
    private void SwitchInventoryBarPosition()
    {
        Vector3 playerViewportPosition = Player.Instance.GetPlayerViewPortPosition();

        if (playerViewportPosition.y > 0.3f && false == IsInventoryBarPositionBottom)
        {
            rectTransform.pivot = new Vector2(0.5f, 0f);

            //改变锚点位置
            rectTransform.anchorMax = new Vector2(0.5f, 0f);
            rectTransform.anchorMin= new Vector2(0.5f, 0f);
            rectTransform.anchoredPosition = new Vector2(0f, 2.5f);
            IsInventoryBarPositionBottom = true;
        }
        else if (playerViewportPosition.y <= 0.3f && true == IsInventoryBarPositionBottom)
        {
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchoredPosition = new Vector2(0f, -2.5f);
            IsInventoryBarPositionBottom = false;
        }
    }

    //清除选框高光
    public void ClearHighlightOnInventorySlots()
    {
        for (int i = 0; i < inventorySlots.Length; ++i)
        {
            inventorySlots[i].inventorySlotHighlight.color = new Color(0f, 0f, 0f, 0f);
            inventorySlots[i].isSelected = false;
        }
    }

    //设置选框高光
    public void SetHighlightedInventorySlots()
    {
        for(int i = 0; i < inventorySlots.Length; ++i)
        {
            if (null != inventorySlots[i].itemDetails)
            {
                if (inventorySlots[i].isSelected)
                {
                    inventorySlots[i].inventorySlotHighlight.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
    }
}

