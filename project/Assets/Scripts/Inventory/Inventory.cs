using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Handle the inventory
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// List of items in the inventory
    /// </summary>
    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    /// <summary>
    /// Event linked to the addition of an item in the inventory
    /// </summary>
    public event EventHandler<InventoryEventArgs> ItemAdded;
    /// <summary>
    /// Event linked to the removal of an item in the inventory
    /// </summary>
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    /// <summary>
    /// Event linked to the utilisation of an item in the inventory
    /// </summary>
    public event EventHandler<InventoryEventArgs> ItemUsed;

    /// <summary>
    /// Add an item in the inventory
    /// </summary>
    /// <param name="item">item to add</param>
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

    /// <summary>
    /// Empty an inventory slot
    /// </summary>
    /// <param name="slot">slot to empty</param>
    /// <param name="item">item in the slot</param>
    public void EmptySlot(GameObject slot, IInventoryItem item)
    {   
        // if the item exists
        if (item != null)
        {
            // we drop the item
            item.OnDrop();
            // we remove the item from the list 
            mItems.Remove(item);
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
                collider.enabled = true;
            // we notify the hud and the player that an item has been removed from the inventory
            if (ItemRemoved != null)
                ItemRemoved(this, new InventoryEventArgs(slot, item));
        }
    }

    /// <summary>
    /// Use an item from the inventory
    /// </summary>
    /// <param name="slot">item slot</param>
    /// <param name="item">item to use</param>
    public void UseItem(GameObject slot, IInventoryItem item)
    {
        // if the item exists
        if (item != null)
        {
            // remove the item from the inventory list
            mItems.Remove(item);
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
                collider.enabled = true;
            // notify the HUD that an item had been used
            ItemUsed(this, new InventoryEventArgs(slot, item));
        }
    }
}
