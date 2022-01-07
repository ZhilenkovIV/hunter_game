using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour, IUnitStateSwitcher
{

    public List<BaseUnitState> states;

    private BaseUnitState currentState;


    // Start is called before the first frame update
    public void Start()
    {
        currentState = new IdleState(new StateInfo(transform, this));
        states = new List<BaseUnitState>();
    }


    public void AddState(BaseUnitState state)
    {
        states.Add(state);
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

    public void SetState<T>() where T : BaseUnitState
    {
        currentState.Exit();
        currentState = states.Find((n) => n is T);
    }

    public void Initialize(BaseUnitState state)
    {
        currentState = state;
        currentState.Entry();
    }
}
