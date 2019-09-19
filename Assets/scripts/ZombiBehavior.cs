using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiBehavior : MonoBehaviour
{
    private Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D col) {
        /*if (col.gameObject.layer == 12) {
            gameObject.layer = 13;
        }*/
    }

    void OnCollisionExit2D(Collision2D col)
    {
        /*if (col.gameObject.layer == 12)
        {
            gameObject.layer = 10;
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
