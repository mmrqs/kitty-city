using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ExcarrotburFoundEventHandler();
/// <summary>
/// Script for the Excarrotbur
/// It allows when the player enters in collision with it to pass to the next level or to 
/// finish the game.
/// </summary>
public class Excarrotbur : MonoBehaviour
{
    /// <summary>
    /// Text asking the player if he wants to extract Excarrotbur.
    /// </summary>
    public GameObject excarrotburText;
    /// <summary>
    /// Boolean indicating if the player can grab Excarrotbur.
    /// </summary>
    private bool grabAllowed;
    /// <summary>
    /// Event advertising that the player has extracted Excarrotbur.
    /// </summary>
    private event ExcarrotburFoundEventHandler ExcarrotburFoundEvent;

    /// <summary>
    /// Allow the subscription to the ExcarrotburFoundEvent.
    /// </summary>
    /// <param name="method"></param>
    public void Subscription(ExcarrotburFoundEventHandler method)
    {
        this.ExcarrotburFoundEvent += method;
    }

    /// <summary>
    /// Start method
    /// desactivates the excarrotbur text
    /// </summary>
    void Start()
    {
        excarrotburText.SetActive(false);    
    }

    /// <summary>
    /// Update method
    /// Raises an event if the player intent to extract Excarrotbur
    /// </summary>
    void Update()
    {
        if (grabAllowed && Input.GetKeyDown(KeyCode.T))
            ExcarrotburFoundEvent?.Invoke();
    }

    /// <summary>
    /// This method is triggered when a body enters in collision with Excarrotbur
    /// If it is the player, we set the text active and allowed the grab.
    /// </summary>
    /// <param name="collisionInfo">information about the collision</param>
    void OnCollisionEnter(Collision collisionInfo)
    {
        // If the carrot is touched by the player
        if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer(Constants.PLAYER_MASK))
        {
            excarrotburText.SetActive(true);
            grabAllowed = true;
        }

    }

    /// <summary>
    /// This method is triggered when a body exit the collision with Excarrotbur.
    /// If it is the player (ie the layer mask equals the player layer mask), we disable the excarrotbur text and the grabbing.
    /// </summary>
    /// <param name="collision">information about the collision</param>
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer(Constants.PLAYER_MASK))
        {
            excarrotburText.SetActive(false);
            grabAllowed = false;
        }
    }
}
