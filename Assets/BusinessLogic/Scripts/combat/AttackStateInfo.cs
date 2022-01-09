using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateInfo : StateInfo {
    public Transform hitObject;
    AttackState nextAttack;
    AttackState endState;

    public AttackStateInfo(Transform unit, IUnitStateSwitcher switcher) : base(unit, switcher)  {
        hitObject = unit;
    }
}