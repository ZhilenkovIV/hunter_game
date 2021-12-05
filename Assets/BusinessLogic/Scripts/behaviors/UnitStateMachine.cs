using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour, IUnitStateSwitcher
{

    private List<BaseUnitState> states;

    public BaseUnitState currentState;

    public UnitStateMachine() {
        states = new List<BaseUnitState>();
    }

    public void AddState(BaseUnitState state) {
        states.Add(state);
    }

    // Start is called before the first frame update
    public void Start()
    {
        currentState = new IdleState(transform, this);
    }

    // Update is called once per frame
    public void Update()
    {
        currentState.LogicUpdate();
    }

    public void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }


    public void SetState(BaseUnitState state) {
        currentState.Exit();

        currentState = state;
        currentState.Entry();
    }

}
