using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Character
{
    [Tooltip("Скорость движения персонажа.")]
    public float moveSpeed = 5f;
    
    public GameObject playerModel;
    
    private Rigidbody2D rb;

    private static PlayerController _instance;
    
    public static PlayerController Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            return null;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 
        float verticalInput = Input.GetAxisRaw("Vertical");   
        
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        
        
        rb.linearVelocity = moveDirection * moveSpeed;
        
        if (horizontalInput > 0)
        {
            playerModel.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontalInput < 0)
        {
            playerModel.GetComponent<SpriteRenderer>().flipX = true; 
        }

    }
}
