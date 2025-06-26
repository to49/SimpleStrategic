using UnityEngine;
using UnityEngine.AI;

public class FsmAgentExample : MonoBehaviour
{
    private StateMachine _stateMachine;
    public NavMeshAgent agent;
    public AnimationController animationController;
    public SpriteRenderer spriteRenderer;

    [SerializeField] private NpcType type;
    [SerializeField] private NpcStateConfig config;
    [SerializeField] private string currentState;
    
    private string targetTag;

    private enum NpcType
    {
        Enemy,
        Friendly
    }
    
    private void Start()
    {
        if (!ValidateComponents())
            return;

        gameObject.GetComponent<Character>().onTakeDamage += ChangeStateToIdle;
        
        targetTag = type == NpcType.Enemy ? "Friendly" : "Enemy";

        agent.updateUpAxis = false;
        agent.updateRotation = false;

        _stateMachine = new StateMachine(this);

        InitializeStates();

        _stateMachine.SetState<FsmStateIdle>();
    }

    private void Update()
    {
        _stateMachine.Update();
        currentState = _stateMachine.CurrentState?.ToString() ?? "None";
    }

    private void InitializeStates()
    {
        if (config == null)
        {
            Debug.LogError("FSM: Отсутствует ссылка на NpcStateConfig!");
            return;
        }

        if (config.enableIdle)
        {
            _stateMachine.AddState(new FsmStateIdle(_stateMachine, agent, transform, targetTag, animationController));
        }

        if (config.enableWalk)
        {
            if (!config.enableAttackRange)
            {
                _stateMachine.AddState(new FsmStateWalk(_stateMachine, agent, animationController, spriteRenderer));
            }
            else
            {
                _stateMachine.AddState(new FsmStateWalk(_stateMachine, agent, config.attackRangeDistance ,animationController, spriteRenderer));
            }
        }

        if (config.enableAttackMelee)
        {
            _stateMachine.AddState(new FsmStateAttackMelee(_stateMachine, config.damage, config.attackCooldown,
                animationController));
        }

        if (config.enableAttackRange)
        {
            IProjectileFactory projectileFactory = gameObject.GetComponent<IProjectileFactory>();
            _stateMachine.AddState(new FsmStateAttackRange(_stateMachine, config.damage, config.attackCooldown, config.attackRangeDistance,
                animationController, config.ProjectilePrefab, projectileFactory, gameObject));
        }
    }

    private bool ValidateComponents()
    {
        bool allValid = true;

        if (agent == null)
        {
            Debug.LogError("FSM: NavMeshAgent не назначен.");
            allValid = false;
        }

        if (animationController == null)
        {
            Debug.LogError("FSM: AnimationController не назначен.");
            allValid = false;
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("FSM: SpriteRenderer не назначен.");
            allValid = false;
        }

        return allValid;
    }

    private void ChangeStateToIdle()
    {
        _stateMachine.SetState<FsmStateIdle>();
        Debug.Log("Событие получения урона");
    }
}