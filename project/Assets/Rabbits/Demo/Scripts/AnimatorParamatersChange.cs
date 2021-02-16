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

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
            jump = new Vector3(0.0f, 2.0f, 0.0f);

        }

        void OnCollisionStay()
        {
            isGrounded = true;
        }

        // Update is called once per frame
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
    }
}
