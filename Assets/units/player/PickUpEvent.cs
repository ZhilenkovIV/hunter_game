using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEvent : MonoBehaviour
{
    private LayerMask layerMask;
    private Collider2D coll;
    public string thingName;

    public static event System.Action<string> Action;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Player");
        coll = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (coll.IsTouchingLayers(layerMask))
        {
            if (Action != null)
            {
                Action(thingName);
            }
            Destroy(gameObject);
        }
    }
}
