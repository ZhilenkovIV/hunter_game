using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveXCommand : ICommand
{
    private readonly Rigidbody2D rb;
    private float direction;
    private float maxSpeed;

    public MoveXCommand(in Rigidbody2D rigidbody, float direction, float maxSpeed) {
        this.maxSpeed = maxSpeed;
        rb = rigidbody;
        this.direction = Mathf.Clamp(direction, -1, 1);
    }

    public void Execute()
    {
        rb.velocity = new Vector2(direction * maxSpeed, rb.velocity.y);
        if (Mathf.Abs(direction) > 0.01) {
            Vector3 theScale = rb.transform.localScale;
            //зеркально отражаем персонажа по оси Х
            theScale.x = Mathf.Abs(theScale.x) * Mathf.Sign(direction);
            //задаем новый размер персонажа, равный старому, но зеркально отраженный
            rb.transform.localScale = theScale;
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

}
