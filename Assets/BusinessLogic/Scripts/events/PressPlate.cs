using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressPlate : MonoBehaviour
{
    public event System.Action Enter;
    public event System.Action Stay;
    public event System.Action Exit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layerColl = collision.collider.gameObject.layer;
        if (collision.collider.tag == "PressPlate") {
            if (Enter != null)
            {
                Enter();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "PressPlate")
        {
            if (Stay != null)
                Stay();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "PressPlate")
        {
            if (Exit != null)
                Exit();
        }
    }
}
