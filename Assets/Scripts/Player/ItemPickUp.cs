
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        if (null != item)
        {
            //获取详细信息
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);
            Debug.Log(itemDetails.itemDescription);

            //如果物品可被捡起
            if (true == itemDetails.canBePickedUp)
            {
                //将其添加到玩家背包中
                InventoryManager.Instance.AddItem(InventoryLocation.player, item,collision.gameObject);
            }
        }
    }
}

