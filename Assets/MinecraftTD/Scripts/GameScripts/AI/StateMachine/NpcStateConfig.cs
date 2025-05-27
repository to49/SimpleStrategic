using UnityEngine;

[CreateAssetMenu(fileName = "NpcStateConfig", menuName = "FSM/NPC State Config")]
public class NpcStateConfig : ScriptableObject
{
    [Header("Состояния NPC")]
    public bool enableIdle;
    public bool enableWalk;
    public bool enableAttackMelee;
    public bool enableAttackRange;

    [Header("Параметры атаки")]
    public int damage = 1;
    public float attackCooldown = 1f;

    [Tooltip("Максимальная дистанция стрельбы (используется только при включенном дальнем бою)")]
    public float attackRangeDistance = 5f;
}