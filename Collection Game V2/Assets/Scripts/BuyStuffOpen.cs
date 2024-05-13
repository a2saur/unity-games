using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyStuffOpen : MonoBehaviour
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

    public void OpenBuy(){
        // Load the specified scene
        // transitionAnimator.SetTrigger("SceneTransition");
        // StartCoroutine(DelayedSceneChange(1f, "BuyItems"));
        SceneManager.LoadScene("BuyItems");
    }

    // IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
    //     yield return new WaitForSecondsRealtime(delay);
    //     SceneManager.LoadScene(sceneToLoad);
    // }
}