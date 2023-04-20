
using UnityEngine;

[System.Serializable]
public class ItemDetails 
{
    public int itemCode;
    public ItemType itemType;
    public string itemDescription;
    public Sprite itemSprite;
    public string itemLongDescription;

    //基于网格的工具半径
    public int itemUseGridRadius;
    //基于距离的工具半径
    public float itemUseRadius;

    public bool isStartingItem;
    public bool canBePickedUp;
    public bool canBeDropped;
    public bool canBeEaten;
    public bool canBeCarried;

}

