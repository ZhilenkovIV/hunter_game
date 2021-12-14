using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : BaseUnitState
{
	public Vector2 destination;
	public Vector2 speed;

	private readonly IMotion motion;
	private readonly Rigidbody2D rb;
    private readonly BaseUnitState _nextState;

    public MoveToPoint(Transform unit, IUnitStateSwitcher switcher, Vector2 destination, BaseUnitState nextState)
        : base(unit, switcher)
    {
        rb = unit.GetComponent<Rigidbody2D>();
        motion = unit.GetComponent<IMotion>();
        _nextState = nextState;
        this.destination = destination;
    }

    public override void Entry()
    {
        
    }

    public override void Exit()
    {

    }

    public override void LogicUpdate()
    {
        if (Vector2.Distance(rb.position, destination) > 0.1f)
        {
            motion.SetSpeed(speed);
        }
        else {
            _switcher.SetState(_nextState);
        }
    }

    public override void PhysicsUpdate()
    {
        motion.Execute();
    }

}
