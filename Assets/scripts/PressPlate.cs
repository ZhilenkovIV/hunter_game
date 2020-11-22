using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressPlate : MonoBehaviour
{
    public LayerMask layerMask;
    public System.Action<string> notify;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layerColl = collision.collider.gameObject.layer;
        if (((1 << layerColl) & layerMask.value) != 0) {
            if(notify != null)
                notify("pressed");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        int layerColl = collision.collider.gameObject.layer;
        if (((1 << layerColl) & layerMask.value) != 0)
        {
            if (notify != null)
                notify("nonpressed");
        }
    }
}
