using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _velocity = 0.5f;
    private int _damage;
    private Transform _target;
    private GameObject _creator;
    private Vector2 _movementDirection;
    private bool _hasDirection;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    public void Initialize(int damage, GameObject target, GameObject creator)
    {
        _damage = damage;
        _creator = creator;
        
        if (target != null)
        {
            _target = target.transform;
            _movementDirection = (_target.position - transform.position).normalized;
            _hasDirection = true;
        }
        else if (!_hasDirection)
        {
            _movementDirection = transform.right;
            _hasDirection = true;
        }
    }
    private void FixedUpdate()
    {
        if (_target != null)
        {
            _movementDirection = (_target.position - transform.position).normalized;
        }
        
        transform.position += (Vector3)_movementDirection * (_velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _creator) return;
        if (other.CompareTag(_creator.tag)) return;

        if (other.TryGetComponent<Character>(out var character))
        {
            character.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
    
}