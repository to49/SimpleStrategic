using UnityEngine;


public class FsmStateIdle : FsmState
{
    private Transform _transform;
    private string _targetTag;
    private GameObject _targetGameObject;
    private AnimationController _animationController;
    private UnityEngine.AI.NavMeshAgent _agent;
    
    public FsmStateIdle(global::StateMachine stateMachine, UnityEngine.AI.NavMeshAgent agent, Transform transform,
        string targetTag, AnimationController animationController)
        : base(stateMachine)
    {
        _transform = transform;
        _targetTag = targetTag;
        _animationController = animationController;
        _agent = agent;
    }

    public override void Enter()
    {
        Debug.Log("Idle state [ENTER]");
        _agent.isStopped = true;
        _animationController.animator.SetBool("IsMoving", false);
        _targetGameObject = FindNearestWithTag(_targetTag, _transform.position);

        if (_targetGameObject != null)
        {
            var walkState = stateMachine.GetState<FsmStateWalk>();
            walkState?.SetTarget(_targetGameObject);
            stateMachine.SetState<FsmStateWalk>();
        }
    }

    public override void Exit()
    {
        Debug.Log("Idle state [EXIT]");
    }

    public override void Update()
    {
        if (_targetGameObject == null)
        {
            _targetGameObject = FindNearestWithTag(_targetTag, _transform.position);
        }

        if (_targetGameObject != null)
        {
            var walkState = stateMachine.GetState<FsmStateWalk>();
            walkState?.SetTarget(_targetGameObject);
            stateMachine.SetState<FsmStateWalk>();
        }
    }

    private GameObject FindNearestWithTag(string tag, Vector3 currentPosition)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in objectsWithTag)
        {
            float distance = Vector3.Distance(currentPosition, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = obj;
            }
        }

        return closest;
    }
}