using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallUnderGround : MonoBehaviour
{
    private LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        EventPickUp.PickUp += (s) =>
        {
            if (s == "fall")
            {
                StartCoroutine(DestroyWithDelay(2));
            }
        };
    }

    IEnumerator DestroyWithDelay(float delta) {
        yield return new WaitForSeconds(delta);
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(playerMask))
        {
            EventPickUp.notify("fall");
        }
    }
}
