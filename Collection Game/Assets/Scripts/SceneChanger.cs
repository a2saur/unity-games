using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // The name of the scene to load when space is pressed
    public string sceneName;

    public Animator transitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = GameObject.FindGameObjectsWithTag("SceneTransition")[0].GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // Load the specified scene
            // animator.Play("SceneTransitionStartStart");
            // animator.SetTrigger("SceneTransition");
            // StartCoroutine(DelayedSceneChange(1f, "MainScene"));
            transitionAnimator.SetTrigger("SceneTransition");
            StartCoroutine(DelayedSceneChange(1f, "MainScene"));
        }
    }

    IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
