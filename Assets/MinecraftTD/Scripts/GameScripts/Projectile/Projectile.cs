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
            UpdateRotation(); 
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
            UpdateRotation(); 
        }
        
        transform.position += (Vector3)_movementDirection * (_velocity * Time.fixedDeltaTime);
    }

    private void UpdateRotation()
    {
        if (_movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(_movementDirection.y, _movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
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