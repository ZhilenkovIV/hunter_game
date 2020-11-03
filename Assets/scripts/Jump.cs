using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpHeight;


    public delegate bool Trigger();
    public Trigger trigger;
    public Trigger stopJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (stopJump != null) {
            stopJump = () => false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger())
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
        if (stopJump() && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
