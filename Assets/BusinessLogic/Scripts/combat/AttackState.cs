using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseUnitState
{
    AttackState nextAttack;
    AttackState FailState;
    System.Func<bool> func;
    float timeWait;

    private float currentTime;
    public AttackState(AttackStateInfo info) : base(info)
    {

    }

    public override void Entry()
    {
        currentTime = 0;
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void LogicUpdate()
    {
        if(currentTime < timeWait){
            if (func()){
                _switcher.SetState(nextAttack);
            }
        } else{
            _switcher.SetState(FailState);
        }
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
