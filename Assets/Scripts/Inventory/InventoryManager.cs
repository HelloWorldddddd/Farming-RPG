using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    
    
    [SerializeField] private SO_ItemList itemList = null; //定义自定义容器

    public List<InventoryItem>[] inventoryLists; //定义背包列表，用于存放不同类型的背包

    public int[] inventoryCapacityIntArray; //定义不同背包容量（例如玩家携带容量、箱子容量）

    private Dictionary<int, ItemDetails> itemDetailsDictionary; //定义字典以使用
    
    private int[] selectedInventoryItem; //定义选中物品数组（数组元素代表各个背包列表中选中的物品的序号）

    protected override void Awake()
    {
        base.Awake();

        CreateItemDetailsDictionary();

        CreateInventoryLists();

        //初始化选中数组
        selectedInventoryItem = new int[(int)InventoryLocation.count];
        for(int i = 0; i < selectedInventoryItem.Length; ++i)
        {
            selectedInventoryItem[i] = -1;
        }
    }

    //创建背包列表
    private void CreateInventoryLists()
    {
        //初始化背包列表
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];
        for(int i = 0; i < (int)InventoryLocation.count; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }

        //初始化背包容量数组
        inventoryCapacityIntArray = new int[(int)InventoryLocation.count];
        //初始化玩家背包容量
        inventoryCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    //从自定义容器读取数据来填充字典
    private void CreateItemDetailsDictionary()
    {
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();

        foreach(var itemDetails in itemList.itemDetails)
        {
            itemDetailsDictionary.Add(itemDetails.itemCode, itemDetails);
        }
    }

    //通过字典查找物品信息
    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemDetails;
        if (itemDetailsDictionary.TryGetValue(itemCode, out itemDetails))
        {
            return itemDetails;
        }
        else return null;
    }

    public string GetItemDescription(ItemType itemType)
    {
        string itemTypeDescription;
        switch (itemType)
        {
            case ItemType.Breaking_tool:
                itemTypeDescription = Settings.BreakingTool;
                break;
            case ItemType.Chopping_tool:
                itemTypeDescription = Settings.ChoppingTool;
                break;
            case ItemType.Hoeing_tool:
                itemTypeDescription = Settings.HoeingTool;
                break;
            case ItemType.Reaping_tool:
                itemTypeDescription = Settings.ReapingTool;
                break;
            case ItemType.Watering_tool:
                itemTypeDescription = Settings.WateringTool;
                break;
            case ItemType.Collecting_tool:
                itemTypeDescription = Settings.CollectingTool;
                break;
            default:
                itemTypeDescription = itemType.ToString();
                break;
        }
        return itemTypeDescription;
    }

    //添加物品方法主体
    public void AddItem(InventoryLocation inventoryLocation,Item item,GameObject thisGameObject)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        //查询该背包中是否已存在该物品
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

        if (-1 != itemPosition)
        {
            AddItemAtPosition(inventoryList,itemCode,itemPosition);
        }
        else
        {
            AddItemAtPosition(inventoryList, itemCode);
        }
        Destroy(thisGameObject);
        //发布者发布事件：背包更新
        EventHandler.CallInventoryUpdateEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    //向指定背包添加物品需要执行的操作（新添加）
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode)
    {
        InventoryItem inventoryItem = new InventoryItem
        {
            itemCode = itemCode,
            itemQuantity = 1
        };
        inventoryList.Add(inventoryItem);
    }
    //向指定背包添加物品需要执行的操作（已存在）
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int itemPosition)
    {
        InventoryItem inventoryItem = new InventoryItem
        {
            itemQuantity = inventoryList[itemPosition].itemQuantity + 1,
            itemCode = itemCode
        };

        inventoryList[itemPosition] = inventoryItem;
    }

    //从指定背包中移除物品
    public void RemoveItem(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);
        if (-1 != itemPosition)
        {
            RemoveItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        EventHandler.CallInventoryUpdateEvent(inventoryLocation, inventoryList);
    }
    //从指定背包中移除物品需要执行的操作
    private void RemoveItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int itemPosition)
    {
        InventoryItem inventoryItem = new InventoryItem();
        int quantity = inventoryList[itemPosition].itemQuantity - 1;
        if (quantity > 0)
        {
            inventoryItem.itemCode = itemCode;
            inventoryItem.itemQuantity = quantity;
            inventoryList[itemPosition] = inventoryItem;
        }
        else
        {
            inventoryList.RemoveAt(itemPosition);
        }
    }

    public int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        for(int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemCode == itemCode)
            {
                return i;
            }
        }
        return -1;
    }

    //交换物品在数据队列中的排列顺序
    internal void SwapInventoryItems(InventoryLocation inventoryLocation, int fromItem, int toItem)
    {
        if(fromItem<inventoryLists[(int)inventoryLocation].Count&& toItem < inventoryLists[(int)inventoryLocation].Count && fromItem != toItem && fromItem >= 0 && toItem >= 0)
        {
            InventoryItem temp = inventoryLists[(int)inventoryLocation][fromItem];
            inventoryLists[(int)inventoryLocation][fromItem] = inventoryLists[(int)inventoryLocation][toItem];
            inventoryLists[(int)inventoryLocation][toItem] = temp;

            EventHandler.CallInventoryUpdateEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
        }
    }

    //对应背包选中物品
    public void SetSelectedInventoryItem(InventoryLocation inventoryLocation,int itemCode)
    {
        selectedInventoryItem[(int)inventoryLocation] = itemCode;
    }

    //撤销选中
    public void ClearSelectedInventoryItem(InventoryLocation inventoryLocation)
    {
        selectedInventoryItem[(int)inventoryLocation] = -1;
    }
}

