using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;

    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += Inventory_ItemRemoved;
        Inventory.ItemUsed += Inventory_ItemUsed;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find(Constants.INVENTORY_LABEL);
        foreach(Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetComponent<Image>();
            ItemDragHandler itemDragHandler = slot.GetChild(0).GetComponent<ItemDragHandler>();

            // We check if this slot is available ie if the image is enabled
            if (!image.enabled)
            {
                // We enabled the image
                image.enabled = true;
                // We add the sprite of the item to this image
                image.sprite = e.Item.Image;
                itemDragHandler.Item = e.Item;
                break;
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        EmptySlot(e.Slot);
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        EmptySlot(e.Slot);
    }

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
