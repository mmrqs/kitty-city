using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handle the drag of an item
/// </summary>
public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public IInventoryItem Item { get; set; }

    /// <summary>
    /// modify the position of the object to match the position of the mouse
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    /// <summary>
    /// Reset the position of the object
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }
}
