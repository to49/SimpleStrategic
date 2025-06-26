using Unity.VisualScripting;
using UnityEngine;

public class FsmStateAttackRange : FsmState
{
    private int _damage;
    private GameObject _targetGameObject;
    private AnimationController _animationController;
    private float _attackCooldown;
    private bool _isFliped;
    private float _timer = 0f;
    private bool _methodCalled = false;
    private Vector3 _attackPointTransform;
    private GameObject _projectilePrefab;
    private GameObject _creator;
    public Projectile projectile; 
    
    private IProjectileFactory _projectileFactory;
    public FsmStateAttackRange(global::StateMachine stateMachine, int damage, float attackCooldown, float attackRangeDistance,
        AnimationController animationController, GameObject projectilePrefab, IProjectileFactory projectileFactory, GameObject creator)
        : base(stateMachine)
    {
        _attackCooldown = attackCooldown;
        _damage = damage;
        _animationController = animationController;
        _projectilePrefab = projectilePrefab;
        _projectileFactory = projectileFactory;
        _creator = creator;
    }
    
    public override void Enter()
    {
        Debug.Log("Attack state [ENTER]");
    }

    public override void Exit()
    {
        Debug.Log("Attack state [EXIT]");
    }

    public override void Update()
    {
        if (_targetGameObject.IsDestroyed())
        {
            stateMachine.SetState<FsmStateIdle>();
        }

        _timer += Time.deltaTime;

        if (_timer >= _attackCooldown && !_methodCalled && !_targetGameObject.IsDestroyed())
        {
            AttackTarget();
            _methodCalled = true;
        }

        if (_methodCalled)
        {
            _timer = 0f;
            _methodCalled = false;
        }
    }

    private void AttackTarget()
    {
        Character targetHealth = _targetGameObject.GetComponent<Character>();
        
        if (targetHealth != null)
        {
            _animationController.AttackAnimation(_isFliped);
            projectile = _projectileFactory.CreateProjectile(_projectilePrefab);
            projectile.Initialize(_damage, _targetGameObject, _creator);
        }

        _methodCalled = false;
    }

    public void SetTarget(GameObject target)
    {
        _targetGameObject = target;
    }

    public void SetFlip(bool isFliped)
    {
        _isFliped = isFliped;
    }
}