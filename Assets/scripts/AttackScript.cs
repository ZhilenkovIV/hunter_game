using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public string attackTag;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals(attackTag)) {
            Debug.Log("get in");
            Collider2D thisCollider = GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(thisCollider, col.collider);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals(attackTag))
        {
            Debug.Log("get out");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
