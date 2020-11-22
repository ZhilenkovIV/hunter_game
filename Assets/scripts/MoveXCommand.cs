using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveXCommand : ICommand
{
    private readonly Rigidbody2D rb;
    private float speed;

    public MoveXCommand(in Rigidbody2D rigidbody, float speed) {
        this.speed = speed;
        rb = rigidbody;
    }

    public void Execute()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (Mathf.Abs(speed) > 0.01) {
            Vector3 theScale = rb.transform.localScale;
            //зеркально отражаем персонажа по оси Х
            theScale.x = Mathf.Abs(theScale.x) * Mathf.Sign(speed);
            //задаем новый размер персонажа, равный старому, но зеркально отраженный
            rb.transform.localScale = theScale;
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

}
