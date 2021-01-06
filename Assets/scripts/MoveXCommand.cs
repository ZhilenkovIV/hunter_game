using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveXCommand : MonoBehaviour, ICommand
{
    private Rigidbody2D rb;

    [HideInInspector]
    public float speed;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
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
