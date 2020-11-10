using System;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public string thingName;
    public LayerMask canPickUp;
    // Start is called before the first frame update

    void Start()
    {
        //target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(canPickUp)) {
            EventPickUp.notify(thingName);
            Destroy(gameObject);
        }
    }
}
