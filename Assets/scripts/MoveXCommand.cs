using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveXCommand : MonoBehaviour, ICommand
{
    private Rigidbody2D rb;
    public float maxSpeed;

    private float direction;
    public float Direction {
        get {
            return direction;
        }
        set {
            direction = Mathf.Clamp(value, -1, 1);
        }
    }

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
        float speed = maxSpeed * direction;
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
