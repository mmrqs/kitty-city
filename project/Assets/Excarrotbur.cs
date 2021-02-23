using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ExcarrotburFoundEventHandler();
public class Excarrotbur : MonoBehaviour
{
    public GameObject excarrotburText;
    private bool grabAllowed;
    private event ExcarrotburFoundEventHandler ExcarrotburFoundEvent;

    public void Subscription(ExcarrotburFoundEventHandler method)
    {
        this.ExcarrotburFoundEvent += method;
    }

    void Start()
    {
        excarrotburText.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        if (grabAllowed && Input.GetKeyDown(KeyCode.T))
            ExcarrotburFoundEvent?.Invoke();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        // If the carrot is touched by the player
        if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer(Constants.PLAYER_MASK))
        {
            excarrotburText.SetActive(true);
            grabAllowed = true;
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer(Constants.PLAYER_MASK))
        {
            excarrotburText.SetActive(false);
            grabAllowed = false;
        }
    }
}
