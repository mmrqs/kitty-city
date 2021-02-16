using UnityEngine;

public class RabbitCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        // Hit by a cat
        if(collisionInfo.collider.name == "cat(Clone)")
        {
            Debug.Log("hit");
        }
    }
}
