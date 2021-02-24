using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 2.0f;
    Rigidbody rb;

    public bool isGrounded;
    public bool isAppleInHand;

    public GameObject hand;

    public Inventory inventory;

    public GameObject cursor;
    public LayerMask layer;
    private Camera cam;

    private Rigidbody rigidBodyApple;

    public int Life { get; protected set; }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        cam = Camera.main;
        Life = Constants.PV_PLAYER;

        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
    }


    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        if(e.Item.Name == "Apple")
        {
            GameObject gameObjectItem = (item as MonoBehaviour).gameObject;
            gameObjectItem.SetActive(true);

            gameObjectItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            gameObjectItem.GetComponent<Rigidbody>().useGravity = false;

            gameObjectItem.transform.parent = hand.transform;
            gameObjectItem.transform.localPosition = gameObjectItem.transform.parent.localPosition;
            gameObjectItem.transform.localRotation = gameObjectItem.transform.parent.localRotation;

            rigidBodyApple = gameObjectItem.GetComponent<Rigidbody>();
            isAppleInHand = true;
        } else if(e.Item.Name == "Strawberry" && Life < Constants.PV_PLAYER)
        {
            Life += 20;
            e.Item.RemoveMySelf();
        }     
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        GameObject gameObjectItem = (item as MonoBehaviour).gameObject;

        gameObjectItem.SetActive(true);

        gameObjectItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        gameObjectItem.GetComponent<Rigidbody>().useGravity = true;
        gameObjectItem.transform.parent = null;
        isAppleInHand = false;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (isAppleInHand)
            LaunchProjectile(rigidBodyApple, cursor, hand.transform, layer);
        else
            cursor.SetActive(false);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        IInventoryItem item = collisionInfo.collider.GetComponent<IInventoryItem>();
        if (item != null)
            inventory.AddItem(item);

        // If the rabbit is touched by an ennemy
        if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer(Constants.ENNEMY_MASK))
            Hurted();
    }

    public void LaunchProjectile(Rigidbody bulletPrefabs, GameObject cursor, Transform shootPoint, LayerMask layer)
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {           
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;
            Vector3 projectileVelocity = CalculateVelocity(hit.point, shootPoint.position, 1f);
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

    private void Hurted()
    {
        Life -= 20;
        rb.AddForce(new Vector3(0.0f, 0.0f, -200.0f), ForceMode.Impulse);
    }
}
