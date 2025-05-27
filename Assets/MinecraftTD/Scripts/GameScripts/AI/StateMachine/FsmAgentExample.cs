using UnityEngine;
using UnityEngine.AI;

public class FsmAgentExample : MonoBehaviour
{
    private StateMachine _stateMachine;

    [Header("Components")] public NavMeshAgent agent;
    public AnimationController animationController;
    public SpriteRenderer spriteRenderer;

    [Header("Configuration")] [SerializeField]
    private NpcType type;

    [SerializeField] private NpcStateConfig config;

    [Header("Debug")] [SerializeField] private string currentState;

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

        targetTag = type == NpcType.Enemy ? "Friendly" : "Enemy";

        agent.updateUpAxis = false;
        agent.updateRotation = false;

        _stateMachine = new StateMachine();

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
            _stateMachine.AddState(new FsmStateWalk(_stateMachine, agent, animationController, spriteRenderer));
        }

        if (config.enableAttackMelee)
        {
            _stateMachine.AddState(new FsmStateAttackMelee(_stateMachine, config.damage, config.attackCooldown,
                animationController));
        }

        if (config.enableAttackRange)
        {
            _stateMachine.AddState(new FsmStateAttackRange(_stateMachine, config.damage, config.attackCooldown,
                animationController));
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
}