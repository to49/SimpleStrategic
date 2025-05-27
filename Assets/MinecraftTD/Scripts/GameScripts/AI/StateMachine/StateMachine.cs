using System;
using System.Collections.Generic;

public class StateMachine
{
    private FsmState StateCurrent { get; set; }
    public FsmState CurrentState => StateCurrent;
    
    private Dictionary<Type, FsmState> _states = new Dictionary<Type, FsmState>();

    public void AddState(FsmState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);
        if (StateCurrent != null && StateCurrent.GetType() == type)
        {
            return;
        }

        if (_states.TryGetValue(type, out var newState))
        {
            StateCurrent?.Exit();

            StateCurrent = newState;

            StateCurrent.Enter();
        }
    }

    public T GetState<T>() where T : FsmState
    {
        if (_states.TryGetValue(typeof(T), out var state))
        {
            return state as T;
        }

        return null;
    }

    public void Update()
    {
        StateCurrent?.Update();
    }
}