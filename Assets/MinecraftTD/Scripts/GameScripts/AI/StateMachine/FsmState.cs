using UnityEngine;

public abstract class FsmState
{
    protected readonly StateMachine stateMachine;
    public FsmState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    
}
 