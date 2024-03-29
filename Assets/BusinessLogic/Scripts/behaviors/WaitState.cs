using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : BaseUnitState
{
    private float currentTime;
    private float time;
    private BaseUnitState nextState;

    public WaitState(StateInfo info, float time, BaseUnitState nextState)
        : base(info)
    {
        this.time = time;
        this.nextState = nextState;
    }

    public override void Entry()
    {
        currentTime = 0;
    }

    public override void Exit()
    {
        
    }

    public override void LogicUpdate()
    {
        if (currentTime < time)
        {
            currentTime += Time.deltaTime;
        }
        else {
            _switcher.SetState(nextState);
        }

    }

    public override void PhysicsUpdate()
    {
        
    }

}
