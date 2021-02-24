using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;

    public void AddItem(IInventoryItem item)
    {
        // if there is enougth space in the inventory, we add the item
        if(mItems.Count < Constants.NB_SLOTS_INVENTORY)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickup();
                // We notify the HUD that an item has been added
                if (ItemAdded != null)
                    ItemAdded(this, new InventoryEventArgs(item));
            }
        }
    }

    public void EmptySlot(GameObject slot, IInventoryItem item)
    {   
        // if the item exists
        if (item != null)
        {
            item.OnDrop();
            mItems.Remove(item);
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
                collider.enabled = true;
            if (ItemRemoved != null)
                ItemRemoved(this, new InventoryEventArgs(slot, item));
        }
    }

    public void UseItem(GameObject slot, IInventoryItem item)
    {
        // if the item exists
        if (item != null)
        {
            mItems.Remove(item);
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
                collider.enabled = true;
            ItemUsed(this, new InventoryEventArgs(slot, item));
        }
    }
}
