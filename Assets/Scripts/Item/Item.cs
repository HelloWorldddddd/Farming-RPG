using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [ItemCodeDescription]
    [SerializeField]
    private int _itemCode;

    private SpriteRenderer spriteRenderer;
    public int ItemCode
    {
        get
        {
            return _itemCode;
        }
        set
        {
            _itemCode = value;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (0 != ItemCode)
        {
            Init(ItemCode);
        }
    }

    private void Init(int itemCode)
    {
        if (0 != itemCode)
        {
            ItemCode = itemCode;
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(ItemCode);
            spriteRenderer.sprite = itemDetails.itemSprite;

            //如果是reapable类型则添加玩家触碰效果脚本
            if (ItemType.Reapable_scenary == itemDetails.itemType)
            {
                gameObject.AddComponent<ItemNudge>();
            }
        }
    }
}

