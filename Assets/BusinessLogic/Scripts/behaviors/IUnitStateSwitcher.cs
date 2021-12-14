using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitStateSwitcher
{

    void Initialize(BaseUnitState state);

    public void SetState(BaseUnitState state);

    public void SetState<T>() where T : BaseUnitState;

}
