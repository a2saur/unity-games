using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingClicks : MonoBehaviour
{
    public CharacterMover cm;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.playing){
            // Detect left mouse button click
            if (Input.GetMouseButtonDown(0))
            {
                // Convert screen position to world position
                Vector3 mouseScreenPos = Input.mousePosition;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

                // If you're in 2D, you might want to set z to 0
                worldPos.z = 0;

                Debug.Log("Mouse clicked at world position: " + worldPos);
                cm.SetTarget(worldPos);
            }
        }
    }
}
