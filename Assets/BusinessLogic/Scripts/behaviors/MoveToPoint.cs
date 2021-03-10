using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
	public Vector2 destination;
	public Vector2 speed;

	private IMotion motion;
	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
    	motion = GetComponent<IMotion>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(rb.position, destination) > 0.1f){
        	motion.SetSpeed(speed);
        	motion.Execute();
        }
    }
}
