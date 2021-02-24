using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory inventory;

    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find(Constants.ITEM_IMAGE_INVENTORY_LABEL).GetComponent<ItemDragHandler>();      
        IInventoryItem item = dragHandler.Item;
        // If there is an item, we use it
        if(item != null)
        {
            inventory.UseItem(gameObject, item);
        }
    }
}
