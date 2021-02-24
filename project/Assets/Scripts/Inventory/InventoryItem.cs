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
    void RemoveMySelf();
}

public class InventoryEventArgs : EventArgs
{
    public IInventoryItem Item;
    public GameObject Slot;

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
}

