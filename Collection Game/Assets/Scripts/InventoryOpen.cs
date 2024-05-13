using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InventoryOpen : MonoBehaviour
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

    public void OpenInventory(){
        // Load the specified scene
        // animator.Play("SceneTransitionStartStart");
        // StartCoroutine(DelayedSceneChange(1f, "MainScene"));
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("CurrentCreatures");
        // transitionAnimator.SetTrigger("SceneTransition");
        // StartCoroutine(DelayedSceneChange(1f, "CurrentCreatures"));
        // Time.timeScale = 1f;
    }

    // IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
    //     yield return new WaitForSecondsRealtime(delay);
    //     SceneManager.LoadScene(sceneToLoad);
    // }
}
