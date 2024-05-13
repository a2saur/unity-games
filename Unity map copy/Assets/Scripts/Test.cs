using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{
    Rigidbody rb;
    public Dictionary <string, int> inventory = new Dictionary<string, int>();
    [SerializeField]
    private TextMeshProUGUI label;
    public GameObject camera;
    
    float xInput;
    float yInput;

    public float newX;
    public float newY;
    
    int jumpCount = 0;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        xInput = Input.GetAxis("Horizontal") * -1;
        yInput = Input.GetAxis("Vertical");

        // rb.AddForce(xInput * speed, 0, zInput * speed);

        float hyp = Mathf.Sqrt((Mathf.Pow(xInput, 2) + Mathf.Pow(yInput, 2))) * Mathf.Sign(xInput);// * Mathf.Sign(yInput);
        float angle = Mathf.Asin(yInput/hyp);

        newY = Mathf.Sin(angle + (camera.transform.localRotation.eulerAngles.y * 0.01745329251f))*hyp;
        angle += 3.14f;
        newX = Mathf.Cos(angle + (camera.transform.localRotation.eulerAngles.y * 0.01745329251f))*hyp;
        // if ((angle + (camera.transform.localRotation.eulerAngles.y * 0.01745329251f)) < 0){
        //     newX *= -1;
        // }
        // Debug.Log("-----");
        // Debug.Log(newX);
        // Debug.Log(newY);

        // if (zInput < 0) {
        rb.AddForce(newX, 0, newY);
        // }

        if(Input.GetKeyDown(KeyCode.Space) && jumpCount == 0)
        {
            rb.AddForce(Vector3.up * 500);
            jumpCount = 1;
        }


        label.text = "";
        foreach(KeyValuePair<string,int> item in inventory){
            label.text += item.Key;
            label.text += ": ";
            label.text += item.Value;
            label.text += "\n";
        }
    }

    private void OnMouseDown()
    {
        // mouse click
    }

    void OnCollisionEnter (Collision hit)
    {
        string itemName = hit.gameObject.name.Replace("(Clone)", "");
        if (hit.gameObject.tag == "Floor") {
            jumpCount = 0;
        }
        if (hit.gameObject.tag == "Item") {
            if (inventory.ContainsKey(itemName) == false) {
                inventory.Add(itemName, 1);
            } else {
                inventory[itemName] = inventory[itemName] + 1;
            }

            hit.gameObject.SetActive(false);
        }
    }
}
