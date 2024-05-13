using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject mainEnemy; // The object that the projectile will swirl around
    public float swirlRadius; // The radius of the swirl orbit
    public float swirlSpeed; // The speed at which the projectile swirls around the main enemy
    public float delayTime; // The amount of time in seconds between each cycle of the swirl and return motion
    public float followSpeed; // The speed at which the projectile follows the main enemy when not swirling

    private float timeStartedSwirling; // The time when the projectile started swirling
    public float maxSwirlTime; // The maximum time the projectile can swirl around the main enemy

    private Vector3 initialPosition; // The starting position of the projectile
    private bool isSwirling = false; // Whether the projectile is currently swirling around the main enemy
    private Vector3 swirlCenter; // The center of the swirl orbit
    private float timeWait;

    void Start()
    {
        initialPosition = transform.position;
        timeWait = 0;
    }

    void Update()
    {
        if (isSwirling)
        {
            SwirlMotion();
        }
        else
        {
            FollowMotion();
            timeWait += Time.deltaTime;
            if (timeWait > delayTime)
            {
                timeWait = 0;
                isSwirling = true;
                swirlCenter = mainEnemy.transform.position;
                timeStartedSwirling = Time.time; // Set the time when the projectile started swirling
                initialPosition = transform.position;
            }
        }
    }

    void FollowMotion()
    {
        // Follow the main enemy
        Vector3 targetPosition = mainEnemy.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void SwirlMotion()
    {
        float elapsedTime = Time.time - timeStartedSwirling; // Calculate how long the projectile has been swirling

        // Swirl motion
        Vector3 swirlPosition = new Vector3(
            swirlCenter.x + Mathf.Sin(elapsedTime * swirlSpeed) * swirlRadius,
            swirlCenter.y + Mathf.Cos(elapsedTime * swirlSpeed) * swirlRadius,
            initialPosition.z
        );
        transform.position = swirlPosition;

        // Check if the projectile has been swirling for a certain amount of time
        if (elapsedTime >= maxSwirlTime)
        {
            isSwirling = false;
        }
    }

    IEnumerator SwirlMotionCoroutine()
    {
        isSwirling = true;
        swirlCenter = mainEnemy.transform.position; // Save the position of the mainEnemy as the center of the swirl orbit
        yield return new WaitForSeconds(delayTime);
    }
}
