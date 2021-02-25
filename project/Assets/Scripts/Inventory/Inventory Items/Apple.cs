using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Models an Apple item
/// </summary>
public class Apple : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Apple";
        }
    }
    /// <summary>
    /// Image associated to the item
    /// </summary>
    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    /// <summary>
    /// Called when the player pickup the item
    /// </summary>
    public void OnPickup()
    {
        // We disable the game object when it is in the inventory
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when the user drop the item
    /// </summary>
    public void OnDrop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if the rat intersect a collider
        if (Physics.Raycast(ray, out hit, 1000))
        {
            // we enable the object and put it on the hit point ie the mouse position of the user
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }

    /// <summary>
    /// Destroy the item
    /// </summary>
    public void RemoveMySelf()
    {
        Destroy(this);
    }
}
