using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiveRabbitsDemo
{
    public class AnimatorParamatersChange : MonoBehaviour
    {

        private Animator m_animator;
        public Vector3 jump;
        public float jumpForce = 2.0f;
        Rigidbody rb;

        private KeyCode previousKey;
        public bool isGrounded;
        public bool isAppleInHand;

        public GameObject hand;

        public Inventory inventory;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
            jump = new Vector3(0.0f, 2.0f, 0.0f);

            inventory.ItemUsed += Inventory_ItemUsed;
            inventory.ItemRemoved += Inventory_ItemRemoved;
        }

        private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
        {
            IInventoryItem item = e.Item;
            GameObject goItem = (item as MonoBehaviour).gameObject;
            goItem.SetActive(true);

            goItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            goItem.GetComponent<Rigidbody>().useGravity = false;
            goItem.transform.parent = hand.transform;
            goItem.transform.localPosition = goItem.transform.parent.localPosition;
            goItem.transform.localRotation = goItem.transform.parent.localRotation;
            isAppleInHand = true;
        }

        private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
        {
            IInventoryItem item = e.Item;
            GameObject goItem = (item as MonoBehaviour).gameObject;

            goItem.SetActive(true);

            goItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            goItem.GetComponent<Rigidbody>().useGravity = true;

            goItem.transform.parent = null;
            isAppleInHand = false;
        }

        void OnCollisionStay()
        {
            isGrounded = true;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z) && previousKey != KeyCode.Z)
            {
                m_animator.SetInteger("AnimIndex", 1);
                m_animator.SetTrigger("Next");

                previousKey = KeyCode.Z;
            }

            if (Input.GetKey(KeyCode.X) && previousKey != KeyCode.X)
            {
                m_animator.SetInteger("AnimIndex", 2);
                m_animator.SetTrigger("Next");
                previousKey = KeyCode.X;
            }

            if (Input.GetKey(KeyCode.A) && previousKey != KeyCode.A)
            {
                m_animator.SetInteger("AnimIndex", 0);
                m_animator.SetTrigger("Next");
                previousKey = KeyCode.A;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        }

        void OnCollisionEnter(Collision collisionInfo)
        {
            IInventoryItem item = collisionInfo.collider.GetComponent<IInventoryItem>();
            if(item != null)
            {
                inventory.AddItem(item);
            }
        }
        
    }
}
