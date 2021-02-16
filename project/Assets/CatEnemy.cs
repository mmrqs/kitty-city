using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class CatEnemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 0.01f;
    private Rigidbody rb;
    Vector3 movement;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();

        m_animator.Play("Base Layer.ldle");
        StartCoroutine(playAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the cat is walking
        if(!m_animator.GetCurrentAnimatorStateInfo(0).IsName("ldle"))
        {
            Vector3 direction = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(direction);
            transform.LookAt(target);
        }
    }

    public IEnumerator playAnimation()
    {
        yield return new WaitForSeconds(3f);
        m_animator.Play("Base Layer.walk");
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == target)
        {
            Debug.Log("hit");
        }
    }
}
