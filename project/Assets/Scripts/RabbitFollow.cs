using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the camera follow the rabbit
/// </summary>
public class RabbitFollow : MonoBehaviour
{
    /// <summary>
    /// target that the camera must follow
    /// </summary>
    public GameObject target;
    /// <summary>
    /// Speed rotation of the camera
    /// </summary>
    public float rotateSpeed = 4;
    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);
        // makes the camera look at the player
        transform.LookAt(target.transform);
    }
}
