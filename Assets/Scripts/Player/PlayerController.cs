using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;


public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;

    [Header("Parameters")]
    [SerializeField] private float moveSpeed; // TODO: come from data
    [SerializeField] private float turnSpeed; // TODO: come from data
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpDuration;


    [Header("Information")]
    [SerializeField] private string currentStateName;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool isGrounded;

    private StateMachine stateMachine;
    private PlayerControls playerControls;
    private CharacterController characterController;
    private Vector2 moveInputVector;
    CountdownTimer jumpTimer;
    private float gravity;
    private float groundingForce;

    void Awake()
    {
        playerControls = new PlayerControls();
        characterController = GetComponent<CharacterController>();

        SetupStateMachine();
    }

    void Start()
    {
        moveSpeed = 5.0f;
        turnSpeed = 2.0f;
        jumpPower = 5.0f;
        jumpDuration = 0.25f;
        gravity = 20f;
        groundingForce = 0.5f;
        currentStateName = "None";

        jumpTimer = new CountdownTimer(jumpDuration);
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

    void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        // states
        var idleState = new IdleState(this, animator);
        var moveState = new MoveState(this, animator);
        var jumpState = new JumpState(this, animator);

        // transitions
        At(idleState, moveState, new FuncPredicate(IsMoving));
        At(idleState, jumpState, new FuncPredicate(IsJumping));
        At(moveState, jumpState, new FuncPredicate(IsJumping));

        Any(idleState, new FuncPredicate(IsIdle));

        // initialize
        stateMachine.SetState(idleState);
    }

    void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    bool IsIdle()
    {
        return (moveInputVector.magnitude == 0f) & (characterController.isGrounded);
    }
    bool IsMoving()
    {
        return (moveInputVector.x > 0f) | (moveInputVector.y > 0f);
    }
    bool IsJumping()
    {
        return !characterController.isGrounded;
    }

    void Update()
    {
        currentStateName = stateMachine.GetCurrentStateName();
        jumpTimer.Tick(Time.deltaTime);
    }

    void LateUpdate()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();

        if(jumpTimer.IsRunning)
        {
            playerVelocity.y = jumpPower;
        }
        else if(characterController.isGrounded)
        {
            playerVelocity.y = -groundingForce;
        }
        else if(!characterController.isGrounded && jumpTimer.IsFinished) 
        {
            playerVelocity.y -= gravity * Time.deltaTime;
        }

        HandleMovement();
        isGrounded = characterController.isGrounded;
    }

    void OnMove(InputValue value) 
    {
        moveInputVector = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (!jumpTimer.IsRunning)
        {
            jumpTimer.Start();
        }
    }

    public void HandleMovement()
    {
        if (characterController.isGrounded) 
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, transform.right * moveInputVector.x, turnSpeed * Mathf.Abs(moveInputVector.x) * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            Vector3 horizontalMoveVector = transform.forward * moveSpeed * moveInputVector.y;
            playerVelocity.x = horizontalMoveVector.x;
            playerVelocity.z = horizontalMoveVector.z;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void HandleJump()
    {
        // noop
        // TODO: check for grounded
        // TODO: check jump timer is off
        // TODO: launch with some velocity
        // TODO: start jump timer
        // TODO: implement state transition possibilities, e.g. can't go from jump to move
    }
}
