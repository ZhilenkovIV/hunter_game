using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Patroler : BaseUnitState
{
	public float speed;
    private float currentSpeed;

	private IMotion motion;
    public float time;
    public float wait;
    private float currentTime;
	public Vector2 point;

    // Start is called before the first frame update
    public Patroler(Transform unit, IUnitStateSwitcher switcher, Vector2 point)
        : base(unit, switcher)
    {
        this.motion = unit.GetComponent<IMotion>();
        this.point = point;
    }

    public override void Entry()
    {
        currentSpeed = speed;
    }

    public override void LogicUpdate()
    {
        if (currentTime < time)
        {
            motion.SetSpeedX(currentSpeed);
        }
        else if (currentTime < time + wait)
        {
            motion.SetSpeedX(0);
        }
        else
        {
            currentTime = 0;
            currentSpeed *= -1;
            motion.SetSpeedX(2 * currentSpeed);
        }
        currentTime += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        motion.Execute();
    }

    public override void Exit()
    {

    }
}
