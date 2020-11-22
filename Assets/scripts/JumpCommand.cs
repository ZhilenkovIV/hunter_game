using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : ICommand
{
    private readonly Rigidbody2D rb;
    private float power;

    public JumpCommand(in Rigidbody2D rigidbody, float jumpHeight) {
        rb = rigidbody;
        power = jumpHeight;
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
