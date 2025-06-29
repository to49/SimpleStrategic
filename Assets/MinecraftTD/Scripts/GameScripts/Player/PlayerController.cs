using UnityEngine;

public class PlayerController : Character
{
    [Tooltip("Скорость движения персонажа.")]
    public float moveSpeed = 5f;
    
    [SerializeField] private AnimationController _animationController;
    public GameObject playerModel;
    public bool isFlipped;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private static PlayerController _instance;
    
    public static PlayerController Instance => _instance;

    private void Awake()
    {
        _instance = this;
        isFlipped = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 
        float verticalInput = Input.GetAxisRaw("Vertical");   
        
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        
        _animationController.animator.SetBool("IsMoving", moveDirection.magnitude > 0.1f);
        
        if (horizontalInput > 0.1f)
        {
            playerModel.GetComponent<SpriteRenderer>().flipX = false;
            isFlipped = false;
        }
        else if (horizontalInput < -0.1f)
        {
            playerModel.GetComponent<SpriteRenderer>().flipX = true; 
            isFlipped = true; 
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Force);

        if (rb.linearVelocity.magnitude > moveSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }
    }
}