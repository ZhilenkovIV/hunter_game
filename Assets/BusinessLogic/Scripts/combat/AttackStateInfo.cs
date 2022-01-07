using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateInfo : StateInfo {
    public Transform hitObject;

    public AttackStateInfo(Transform unit, IUnitStateSwitcher switcher) : base(unit, switcher)  {
        hitObject = unit;
    }
}