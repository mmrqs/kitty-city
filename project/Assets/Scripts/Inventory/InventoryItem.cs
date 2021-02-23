using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickup();
    void OnDrop();
    void OnUse();
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }

    public InventoryEventArgs(GameObject slot)
    {
        Slot = slot;
    }

    public InventoryEventArgs(GameObject slot, IInventoryItem item)
    {
        Slot = slot;
        Item = item;
    }
    public IInventoryItem Item;
    public GameObject Slot;
}

