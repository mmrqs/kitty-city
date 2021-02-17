using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private const int NBSLOTS = 10;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;

    public void AddItem(IInventoryItem item)
    {
        if(mItems.Count < NBSLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickup();
                if (ItemAdded != null)
                    ItemAdded(this, new InventoryEventArgs(item));
            }
        }
    }

    public void EmptySlot(GameObject slot, IInventoryItem item)
    {     
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
