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
        Transform inventoryPanel = transform.Find("Inventory");
        foreach(Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetComponent<Image>();
            ItemDragHandler itemDragHandler = slot.GetChild(0).GetComponent<ItemDragHandler>();

            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                itemDragHandler.Item = e.Item;
                break;
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Image image = e.Slot.transform.GetChild(0).GetComponent<Image>();
        ItemDragHandler itemDragHandler = e.Slot.transform.GetChild(0).GetComponent<ItemDragHandler>();
        image.enabled = false;
        image.sprite = null;
        itemDragHandler.Item = null;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        Image image = e.Slot.transform.GetChild(0).GetComponent<Image>();
        ItemDragHandler itemDragHandler = e.Slot.transform.GetChild(0).GetComponent<ItemDragHandler>();
        image.enabled = false;
        image.sprite = null;
        itemDragHandler.Item = null;
    }
}
