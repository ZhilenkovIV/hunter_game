using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInfo{
    public readonly IUnitStateSwitcher switcher;
    public readonly Transform unit;

    public StateInfo(Transform unit, IUnitStateSwitcher switcher) {
        this.unit = unit;
        this.switcher = switcher;
    }
}