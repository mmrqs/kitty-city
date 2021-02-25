using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Manage the inventory
/// </summary>
public class HUD : MonoBehaviour
{
    /// <summary>
    /// Inventory to manage
    /// </summary>
    public Inventory Inventory;

    /// <summary>
    /// Start method
    /// subscribe its method to the inventory events
    /// </summary>
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += Inventory_ItemRemoved;
        Inventory.ItemUsed += Inventory_ItemUsed;
    }

    /// <summary>
    /// manage the addition of an item in the inventory
    /// </summary>
    /// <param name="sender">event sender</param>
    /// <param name="e"></param>
    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find(Constants.INVENTORY_LABEL);
        // foreach slot in the inventory
        foreach(Transform slot in inventoryPanel)
        {
            // we get the image in the slot
            Image image = slot.GetChild(0).GetComponent<Image>();
            ItemDragHandler itemDragHandler = slot.GetChild(0).GetComponent<ItemDragHandler>();

            // We check if this slot is available ie if the image is enabled
            if (!image.enabled)
            {
                // We enabled the image
                image.enabled = true;
                // We add the sprite of the item to this image
                image.sprite = e.Item.Image;
                // we add the item to the drag handler
                itemDragHandler.Item = e.Item;
                break;
            }
        }
    }

    /// <summary>
    /// Manage the removal of an item in the inventory
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        EmptySlot(e.Slot);
    }

    /// <summary>
    /// Manage the utilisation of an item in the inventory
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        EmptySlot(e.Slot);
    }

    /// <summary>
    /// Empty a slot
    /// </summary>
    /// <param name="slot">slot to empty</param>
    private void EmptySlot(GameObject slot)
    {
        // We get the image of the slot
        Image image = slot.transform.GetChild(0).GetComponent<Image>();
        ItemDragHandler itemDragHandler = slot.transform.GetChild(0).GetComponent<ItemDragHandler>();
        // We desable the image
        image.enabled = false;
        // We remove the sprite image
        image.sprite = null;
        // We cancel the drag
        itemDragHandler.Item = null;
    }
}
