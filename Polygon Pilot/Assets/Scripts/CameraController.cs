using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float followDistance = 10.0f;
    public float followHeight = 3.0f;
    public float followDamping = 5.0f;

    private Vector3 m_FollowPosition;
    private Quaternion m_FollowRotation;

    public Transform target;    // The Transform component of the plane that the camera will follow
    public Vector3 offset;      // The offset between the camera and the plane

    void LateUpdate()
    {
        // Move the camera to the target position with the specified offset
        // transform.position = target.position + offset;
        // transform.position = target.position - (target.GetComponent<PlaneShape>().moveDirection * 10);

        // Make the camera look at the target
        m_FollowPosition = target.position - target.GetComponent<PlaneShape>().cameraDirection * followDistance + Vector3.up * followHeight;
        transform.position = Vector3.Lerp(transform.position, m_FollowPosition, Time.deltaTime * followDamping);

        m_FollowRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
        m_FollowRotation *= Quaternion.Euler(0f, 0f, 90f);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_FollowRotation, Time.deltaTime * followDamping);
        // transform.LookAt(target);
    }
}
