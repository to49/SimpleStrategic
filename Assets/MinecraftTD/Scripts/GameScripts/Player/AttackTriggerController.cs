using Unity.VisualScripting;
using UnityEngine;

public class AttackTriggerController : MonoBehaviour
{
    [SerializeField] private AnimationController animationController;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private PlayerController playerController;
    private float _timer = 0f;
    private bool methodCalled = false;
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Character enemyCharacter = other.gameObject.GetComponent<Character>();
            _timer += Time.deltaTime;

            if (_timer >= attackCooldown && !methodCalled && !other.gameObject.IsDestroyed())
            {
                AttackTarget(enemyCharacter, playerController.isFlipped);
                methodCalled = true;
            }

            if (methodCalled)
            {
                _timer = 0f;
                methodCalled = false;
            }
        }
    }

    void AttackTarget(Character target, bool isFlipped)
    {
        animationController.AttackAnimation(isFlipped);
        target.TakeDamage(damage);
    }
}
