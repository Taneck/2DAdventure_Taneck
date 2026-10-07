using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private  Rigidbody2D rb;
    public Vector2 inputDirection;

    public float speed; 
    
    private void Awake()
    {
        inputControl = new PlayerInputControl();

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
        {
            faceDir = 1;
        }
        else if (inputDirection.x < 0)
        {
            faceDir = -1;
        }

        // Character flipping
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
}