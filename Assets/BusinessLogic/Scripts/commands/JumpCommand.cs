using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : MonoBehaviour, ICommand
{
    private Rigidbody2D rb;
    public float power;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
        rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
        
    }


    public void Undo()
    {
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
