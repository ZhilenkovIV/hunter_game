using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnitState
{
    protected readonly IUnitStateSwitcher _switcher;
    protected readonly Transform _unit;


    protected BaseUnitState(Transform unit, IUnitStateSwitcher switcher) {
        _unit = unit;
        _switcher = switcher;
    }

    public abstract void Entry();

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void Exit();


}
