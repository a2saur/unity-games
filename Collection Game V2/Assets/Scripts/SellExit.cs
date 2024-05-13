using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SellExit : MonoBehaviour
{
    public Animator transitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = GameObject.FindGameObjectsWithTag("SceneTransition")[0].GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Done()
    {
        // SceneManager.LoadScene("ShopScene");
        transitionAnimator.SetTrigger("SceneTransition");
        StartCoroutine(DelayedSceneChange(1f, "ShopScene"));
    }

    IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}