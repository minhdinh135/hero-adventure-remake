using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpImpulse = 10f;

    [SerializeField]
    private float currentSpeed;

    Vector2 moveInput;
    TouchingDirections touchingDirections;

    [SerializeField]
    private bool _isMoving = false;

    [SerializeField]
    private bool _isRunning = false;

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        currentSpeed = GetCurrentSpeed();

        rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        FlipSprite();
    }

    private float GetCurrentSpeed()
    {
        if (_isMoving && !touchingDirections.IsOnWall)
        {
            if (_isRunning)
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }
        else
        {
            return 0;
        }

        return currentSpeed;
    }

    private void FlipSprite()
    {
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if(moveInput.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        _isMoving = moveInput != Vector2.zero;

        animator.SetBool(AnimationStrings.isMoving, _isMoving);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _isRunning = true;
        }
        else if(context.canceled)
        {
            _isRunning = false;
        }

        animator.SetBool(AnimationStrings.isRunning, _isRunning);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.y, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }
}
