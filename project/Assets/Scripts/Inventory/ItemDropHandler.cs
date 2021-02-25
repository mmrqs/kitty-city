using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handle the drop of an object
/// </summary>
public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public IInventoryItem Item { get; set; }
    public Inventory inventory;

    /// <summary>
    /// Drop the item
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform inventoryPanel = transform as RectTransform;

        // check if the mouse is outside the inventory pannel
        if(!RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, Input.mousePosition))
        {
            // we get the item
            ItemDragHandler itemDragHandler = eventData.selectedObject.transform.GetChild(0).GetComponent<ItemDragHandler>();
            IInventoryItem item = itemDragHandler.Item;
            // we get the selected slot
            GameObject selectedSlot = eventData.selectedObject;
            // we empty the slot
            inventory.EmptySlot(selectedSlot, item);
        }
    }
}
