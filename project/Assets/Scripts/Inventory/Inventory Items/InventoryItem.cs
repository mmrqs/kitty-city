using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Interface of inventory item
/// </summary>
public interface IInventoryItem
{
    /// <summary>
    /// Name of the item
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Image associated with the item
    /// </summary>
    Sprite Image { get; }
    /// <summary>
    /// Called when the user pickup the item
    /// </summary>
    void OnPickup();
    /// <summary>
    /// Called when the user drop the item
    /// </summary>
    void OnDrop();
    /// <summary>
    /// Remove the gameobject from the game
    /// </summary>
    void RemoveMySelf();
}

/// <summary>
/// Event associated with the inventory
/// </summary>
public class InventoryEventArgs : EventArgs
{
    /// <summary>
    /// Inventory item
    /// </summary>
    public IInventoryItem Item;
    /// <summary>
    /// Slot in the inventory
    /// </summary>
    public GameObject Slot;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="item">inventory item</param>
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="slot">inventory slot</param>
    public InventoryEventArgs(GameObject slot)
    {
        Slot = slot;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="slot">inventory slot</param>
    /// <param name="item">item</param>
    public InventoryEventArgs(GameObject slot, IInventoryItem item)
    {
        Slot = slot;
        Item = item;
    }
}

