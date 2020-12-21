using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMotion : MonoBehaviour
{
    public PressPlate pressPlate;
    private Rigidbody2D rb;
    public float y0;
    public float y1;

    public float speed;

    public bool inMoving = false;
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pressPlate.notify += (n) =>
        {
            if (n == "enter")
            {
                Debug.Log("Check");
                if (!inMoving) {
                    inMoving = true;
                }
            }
        };
    }

    private void FixedUpdate()
    {
        if (inMoving) {
            rb.velocity = new Vector2(0, speed);
            if (
                Mathf.Abs(rb.position.y - y1) <= 0.1) {
                inMoving = false;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(transform.position.x, y0, transform.position.z),
            new Vector3(transform.position.x, y1, transform.position.z));
    }
}
