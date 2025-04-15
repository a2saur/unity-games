using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object to follow
    public Vector3 offset; // Offset from the target

    public bool smoothing = false;
    public float smoothSpeed = 0.15f;
    
    void Update()
    {
        if (smoothing){
            transform.position = Vector3.Lerp(transform.position, target.position+offset, smoothSpeed);
        } else {
            transform.position = target.position+offset;
        }
    }
}
