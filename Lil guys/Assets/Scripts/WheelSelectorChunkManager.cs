using UnityEngine;
using UnityEngine.UI;

public class WheelSelectorChunkManager : MonoBehaviour
{
    public int id;
    private Animator anim;
    public bool selected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverEnter(){
        anim.SetBool("hover", true);
    }

    public void HoverExit(){
            anim.SetBool("hover", false);
    }

    public void Select(){
        selected = true;
    }
}
