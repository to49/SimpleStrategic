using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class FsmStateWalk : FsmState
{
    private NavMeshAgent _agent;
    private SpriteRenderer _spriteRenderer;
    private GameObject _targetGameObject;
    private AnimationController _animationController;
    private bool _isFliped = false;
    public FsmStateWalk(global::StateMachine stateMachine, NavMeshAgent agent, AnimationController animationController,
        SpriteRenderer spriteRenderer)
        : base(stateMachine)
    {
        _spriteRenderer = spriteRenderer;
        _agent = agent;
        _animationController = animationController;
        _isFliped = false;
    }
    
    public override void Enter()
    {
        Debug.Log("Walk state [ENTER]");
        _animationController.animator.SetBool("IsMoving", true);
    }

    public override void Exit()
    {
        Debug.Log("Walk state [EXIT]");
        _animationController.animator.SetBool("IsMoving", false);
    }

    public override void Update()
    {
        if (_targetGameObject.IsDestroyed())
        {
            stateMachine.SetState<FsmStateIdle>();
        }
        else
        {
            if (_isFliped == false && _agent.transform.position.x > _targetGameObject.transform.position.x)
            {
                _spriteRenderer.flipX = true;
                _isFliped = true;
            }
            else if (_isFliped == true && _agent.transform.position.x < _targetGameObject.transform.position.x)
            {
                _spriteRenderer.flipX = false;
                _isFliped = false;
            }

            if (_targetGameObject == null)
            {
                stateMachine.SetState<FsmStateIdle>();
                return;
            }

            float distance = Vector3.Distance(_agent.transform.position, _targetGameObject.transform.position);

            if (distance < 0.9f)
            {
                _agent.isStopped = true;
                var attackState = stateMachine.GetState<FsmStateAttackMelee>();
                attackState?.SetTarget(_targetGameObject);
                attackState?.SetFlip(_isFliped);
                stateMachine.SetState<FsmStateAttackMelee>();
            }
            else
            {
                _agent.isStopped = false;
                _agent.SetDestination(_targetGameObject.transform.position);
                Debug.Log(_targetGameObject.name);
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        _targetGameObject = target;
    }
}