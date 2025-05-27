using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class FsmStateAttackMelee : FsmState
{
    private int _damage;
    private GameObject _targetGameObject;
    private AnimationController _animationController;
    private float _attackCooldown;
    private bool _isFliped;
    private float _timer = 0f;
    private bool methodCalled = false;
    
    public FsmStateAttackMelee(global::StateMachine stateMachine, int damage, float attackCooldown,
        AnimationController animationController)
        : base(stateMachine)
    {
        _attackCooldown = attackCooldown;
        _damage = damage;
        _animationController = animationController;
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

        if (_timer >= _attackCooldown && !methodCalled && !_targetGameObject.IsDestroyed())
        {
            AttackTarget();
            methodCalled = true;
        }

        if (methodCalled)
        {
            _timer = 0f;
            methodCalled = false;
        }
    }

    private void AttackTarget()
    {
        Character targetHealth = _targetGameObject.GetComponent<Character>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(_damage);
            _animationController.AttackAnimation(_isFliped);
            Debug.Log($"Нанесено {_damage} урона {_targetGameObject.name}!");
        }

        methodCalled = false;
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