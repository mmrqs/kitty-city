using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script managing the player
/// </summary>
public class Player : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 2.0f;

    private Rigidbody rigidBodyPlayer;

    /// <summary>
    /// Indicates if the player is at ground level
    /// </summary>
    public bool isGrounded;
    /// <summary>
    /// Indicates if the player has an apple in the hand
    /// </summary>
    public bool isAppleInHand;

    /// <summary>
    /// Simulates the hand of the player
    /// </summary>
    public GameObject hand;

    public Inventory inventory;

    /// <summary>
    /// Cursor for the shooting ie indicates where the projectile will be shot
    /// </summary>
    public GameObject cursor;
    /// <summary>
    /// Masks that indicates on which surface the player can shoot the projectile
    /// </summary>
    public LayerMask layer;
    /// <summary>
    /// Main camera
    /// </summary>
    private Camera cam;

    private Rigidbody rigidBodyApple;
    /// <summary>
    /// Life of the player
    /// </summary>
    public int Life { get; protected set; }


    /// <summary>
    /// Start method
    /// initialize the rigid body of the player, 
    /// the jump fector, the main camera, the life of the player and subscribes the method Inventory_ItemUsed 
    /// and ItemRemoved respectively to the ItemUsed event and ItemRemoved of the inventory
    /// </summary>
    void Start()
    {
        rigidBodyPlayer = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        cam = Camera.main;
        Life = Constants.PV_PLAYER;

        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
    }

    /// <summary>
    /// Is triggered when the user uses an item
    /// </summary>
    /// <param name="sender">sender of the event</param>
    /// <param name="e"></param>
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        // if the item used is an apple
        if(e.Item.Name == Constants.APPLE_OBJECT)
        {
            GameObject gameObjectItem = (item as MonoBehaviour).gameObject;
            //We set the game object active
            gameObjectItem.SetActive(true);

            gameObjectItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            gameObjectItem.GetComponent<Rigidbody>().useGravity = false;
            //We put the apple in the hand of the player
            gameObjectItem.transform.parent = hand.transform;
            gameObjectItem.transform.localPosition = gameObjectItem.transform.parent.localPosition;
            gameObjectItem.transform.localRotation = gameObjectItem.transform.parent.localRotation;

            rigidBodyApple = gameObjectItem.GetComponent<Rigidbody>();
            isAppleInHand = true;
        }
        // If the item used is a strawberry
        else if(e.Item.Name == Constants.STRAWBERRY_OBJECT && Life < Constants.PV_PLAYER)
        {
            // We add 20 pv to the player life
            Life += 20;
            // We delete the game object
            e.Item.RemoveMySelf();
        }     
    }

    /// <summary>
    /// Is triggered when the user remove an item from his inventory
    /// </summary>
    /// <param name="sender">event sender</param>
    /// <param name="e"></param>
    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        // We get the item and game object
        IInventoryItem item = e.Item;
        GameObject gameObjectItem = (item as MonoBehaviour).gameObject;

        // We set the game object active
        gameObjectItem.SetActive(true);

        gameObjectItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObjectItem.GetComponent<Rigidbody>().useGravity = true;
        gameObjectItem.transform.parent = null;
        isAppleInHand = false;
    }

    /// <summary>
    /// Update method.
    /// </summary>
    void Update()
    {
        // if the player press the space and the player is at ground level
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // we make the player jump
            rigidBodyPlayer.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        // if the player has an apple in his hand
        if (isAppleInHand)
            LaunchProjectile(rigidBodyApple, cursor, hand.transform, layer);
        else
            cursor.SetActive(false);
    }

    /// <summary>
    /// Is triggered when the player enters in collision with another body
    /// </summary>
    /// <param name="collisionInfo">information about the collision</param>
    void OnCollisionEnter(Collision collisionInfo)
    {
        IInventoryItem item = collisionInfo.collider.GetComponent<IInventoryItem>();
        // if this is an item that can be collected, we add it to the inventory
        if (item != null)
            inventory.AddItem(item);

        // If the rabbit is touched by an ennemy
        if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer(Constants.ENNEMY_MASK))
            Hurted();
    }

    /// <summary>
    /// Allow a player to launch a projectile
    /// </summary>
    /// <param name="bulletPrefabs">projectile to launch</param>
    /// <param name="cursor">cursor indicating where to launch the projectile</param>
    /// <param name="shootPoint">indicates from where shooting the projectile</param>
    /// <param name="layer">mask indicating where a projectile can be launch</param>
    public void LaunchProjectile(Rigidbody bulletPrefabs, GameObject cursor, Transform shootPoint, LayerMask layer)
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // if the projectile can be launch here
        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;
            // we calculate the velocity of the projectile
            Vector3 projectileVelocity = CalculateVelocity(hit.point, shootPoint.position, 1f);
            // if the player press the left button of the mouse, we launch the projectile
            if (Input.GetMouseButtonDown(0))
            {
                bulletPrefabs.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                bulletPrefabs.GetComponent<Rigidbody>().useGravity = true;
                bulletPrefabs.transform.parent = null;
                isAppleInHand = false;
                bulletPrefabs.velocity = projectileVelocity;
            }
        }
        else
            cursor.SetActive(false);
    }

    /// <summary>
    /// calculates the velocity
    /// </summary>
    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }

    /// <summary>
    /// If the player is hurted, took 20 of his point of life and we make the player step back
    /// </summary>
    private void Hurted()
    {
        Life -= 20;
        rigidBodyPlayer.AddForce(new Vector3(0.0f, 0.0f, -200.0f), ForceMode.Impulse);
    }

    /// <summary>
    /// Is trigger if the player stays in collision with a body
    /// </summary>
    void OnCollisionStay()
    {
        isGrounded = true;
    }
}
