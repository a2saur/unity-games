using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseAttributes : MonoBehaviour
{
    public string residentName;
    // public FurnitureAttributes[] items;

    public Animator transitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = GameObject.FindGameObjectsWithTag("SceneTransition")[0].GetComponent<Animator>();   
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnCollisionEnter2D(Collision2D hit){
        if (hit.gameObject.tag == "Player") {
            // SceneManager.LoadScene(resident.residentName+"House");
            transitionAnimator.SetTrigger("SceneTransition");
            Debug.Log("A");
            StartCoroutine(DelayedSceneChange(1f, residentName+"House"));
        }
    }

    IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
        yield return new WaitForSecondsRealtime(delay);
        Debug.Log("B");
        SceneManager.LoadScene(sceneToLoad);
    }
}