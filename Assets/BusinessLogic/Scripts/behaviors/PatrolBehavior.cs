using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isPatroling = true;

    public float speed;
    public float radius;
    public Vector2 currentDirection;
    private Vector2 meanPoint;

    public IMotion motion;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        motion = GetComponent<IMotion>();
        meanPoint = rb.position + Random.Range(-radius, radius) * currentDirection ;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatroling) {
            if ((meanPoint - rb.position).magnitude > radius - 0.01f) {
                speed = -speed;
                
            }
            rb.velocity = speed * currentDirection.normalized;
            //transform += Cu
        }
    }
}
