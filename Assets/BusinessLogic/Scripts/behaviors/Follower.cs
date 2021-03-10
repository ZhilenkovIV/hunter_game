using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxSpeed;

    private Rigidbody2D rb;
    public Rigidbody2D target;
    private IMotion motion;

    // Start is called before the first frame update
    void Start()
    {
        motion = GetComponent<IMotion>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target){
			Vector2 distance = target.position - rb.position;
        	Vector2 dir = new Vector2(Mathf.Sign(distance.x), Mathf.Sign(distance.y));
        	motion.SetSpeed(dir * maxSpeed);
            motion.Execute();
        }
    }

    public void SetTarget(Rigidbody2D target){
        this.target = target;
    }

    public void ClearTarget(){
        target = null;
    }
}
