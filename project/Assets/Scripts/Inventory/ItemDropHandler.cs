using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public IInventoryItem Item { get; set; }
    public Inventory inventory;

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;
        if(!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            ItemDragHandler itemDragHandler = eventData.selectedObject.transform.GetChild(0).GetComponent<ItemDragHandler>();
            IInventoryItem item = itemDragHandler.Item;

            GameObject selectedSlot = eventData.selectedObject;

            inventory.EmptySlot(selectedSlot, item);
        }
    }
}
