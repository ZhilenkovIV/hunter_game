using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseUnitState
{

    IMotion motion;
    public IdleState(StateInfo info)
        : base(info)
    {
        motion = info.unit.GetComponent<IMotion>();
    }

    public override void Entry()
    {
        motion?.SetSpeedX(0);
    }

    public override void Exit()
    {
        
    }

    public override void LogicUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        
    }

}
