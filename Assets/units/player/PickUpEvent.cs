using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEvent : MonoBehaviour
{
    private LayerMask layerMask;
    public string thingName;

    public static event System.Action<string> Action;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layerColl = collision.collider.gameObject.layer;
        if (((1 << layerColl) & layerMask.value) != 0)
        {
            if (Action != null)
            {
                Action(thingName);
            }
            Destroy(gameObject);
        }
    }
}
