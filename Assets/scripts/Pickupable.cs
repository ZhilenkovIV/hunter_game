using System;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public string thingName;
    private LayerMask playerMask;
    // Start is called before the first frame update

    void Start()
    {
        playerMask = LayerMask.GetMask("Player");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(playerMask)) {
            EventPickUp.notify(thingName);
            Destroy(gameObject);
        }
    }
}
