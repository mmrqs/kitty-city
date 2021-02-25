using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the click on an item of the inventory
/// </summary>
public class ItemClickHandler : MonoBehaviour
{
    public Inventory inventory;

    /// <summary>
    /// Triggered when the user click on an item of the inventory
    /// </summary>
    public void OnItemClicked()
    {
        // we get the item
        ItemDragHandler dragHandler = gameObject.transform.Find(Constants.ITEM_IMAGE_INVENTORY_LABEL).GetComponent<ItemDragHandler>();      
        IInventoryItem item = dragHandler.Item;
        // If there is an item, we use it
        if(item != null)
            inventory.UseItem(gameObject, item);
    }
}
