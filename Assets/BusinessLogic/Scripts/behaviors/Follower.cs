using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : BaseUnitState
{
    public float maxSpeed;

    private Rigidbody2D rb;
    public Rigidbody2D target;
    private IMotion motion;

    // Start is called before the first frame update
    public Follower(Transform transfrom, IUnitStateSwitcher switcher)
        : base(transfrom, switcher)
    {

        motion = transfrom.GetComponent<IMotion>();
        rb = transfrom.GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Rigidbody2D target){
        this.target = target;
    }

    public void ClearTarget(){
        target = null;
    }

    public override void Entry()
    {
        
    }

    public override void LogicUpdate()
    {
        if (target)
        {
            Vector2 distance = target.position - rb.position;
            Vector2 dir = new Vector2(Mathf.Sign(distance.x), Mathf.Sign(distance.y));
            motion.SetSpeed(dir * maxSpeed);
        }
    }

    public override void PhysicsUpdate()
    {
        motion.Execute();
    }

    public override void Exit()
    {
        motion.SetSpeed(new Vector2(0, rb.velocity.y));
    }
}
