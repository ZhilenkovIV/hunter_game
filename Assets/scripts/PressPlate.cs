using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressPlate : MonoBehaviour
{
    public System.Action<string> notify;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layerColl = collision.collider.gameObject.layer;
        if (collision.collider.tag == "PressPlate") {
            if(notify != null)
                notify("enter");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "PressPlate")
        {
            if (notify != null)
                notify("stay");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "PressPlate")
        {
            if (notify != null)
                notify("exit");
        }
    }
}
