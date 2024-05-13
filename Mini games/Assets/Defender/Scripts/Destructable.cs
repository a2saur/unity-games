using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public Ship ship;
    public int amount;
    public GameObject effectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Laser") {
            GameObject explosion = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<ParticleSystem>().Play();
            
            ship.score += amount;
            
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
    }
}
