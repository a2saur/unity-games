using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformObject : MonoBehaviour
{
    public float maxXScale;
    public float maxYScale;
    public Color originalColor;
    public bool selected = false;
    public bool rotating = false;

    private SpriteRenderer spriteRenderer;
    private float posX;
    private float posY;
    private float scaleX;
    private float scaleY;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.transforming){
            // Check if selected
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (Input.GetMouseButtonDown(0)){
                if (hit.collider != null && hit.collider.gameObject == gameObject){
                    // Debug.Log("CLICKED!");
                    selected = !selected;
                    if (selected){
                        posX = mousePosition.x;
                        posY = mousePosition.y;
                        scaleX = transform.localScale.x;
                        scaleY = transform.localScale.y;
                    }
                } else {
                    selected = false;
                    rotating = false;
                }
            }

            if (selected){
                if (rotating){
                    // rotating
                    spriteRenderer.color =new Color(0.5f,0,1f,1); // purple

                    // Quaternion rotation = Quaternion.LookRotation(mousePosition, Vector3.up);
                    // rotation.x = 0;
                    // rotation.y = 0;
                    // transform.rotation = rotation;
                    float dX = mousePosition.x-transform.position.x;
                    float dY = mousePosition.y-transform.position.y;
                    float calc = Mathf.Rad2Deg*Mathf.Atan(dY/dX);
                    transform.rotation = Quaternion.Euler(0, 0, calc);

                    if (Input.GetKeyDown(Controller.swapButton)){
                        rotating = false;
                        posX = mousePosition.x;
                        posY = mousePosition.y;
                        scaleX = transform.localScale.x;
                        scaleY = transform.localScale.y;
                    }
                } else {
                    // scaling
                    spriteRenderer.color = new Color(1,0,0.5f,1); // pink
                    float xScale;
                    if (Controller.numStrands > 0){
                        xScale = scaleX*1+(posX-mousePosition.x);
                        if (xScale > maxXScale){
                            xScale = maxXScale;
                        }
                    } else {
                        xScale = scaleX;
                    }

                    float yScale;
                    if (Controller.numStrands > 1){
                        yScale = scaleY*1+(posY-mousePosition.y);
                        if (yScale > maxYScale){
                            yScale = maxYScale;
                        }
                    } else {
                        yScale = scaleY;
                    }

                    transform.localScale = new Vector2(xScale, yScale);

                    if (Controller.numStrands > 2 && Input.GetKeyDown(Controller.swapButton)){
                        rotating = true;
                    }
                }
            } else {
                spriteRenderer.color = new Color(0.25f,1,0.75f,1); // cyan
            }
        } else {
            selected = false;
            rotating = false;
            spriteRenderer.color = originalColor;
        }
    }
}