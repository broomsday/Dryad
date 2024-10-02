using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;


public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;
    [SerializeField] CharacterData characterData;


    [Header("Information")]
    [SerializeField] private string currentStateName;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool isGrounded;

    private StateMachine stateMachine;
    private PlayerControls playerControls;
    private CharacterController characterController;
    private CharacterMovement characterMovement;
    private Vector2 moveInputVector;
    CountdownTimer jumpTimer;

    void Awake()
    {
        playerControls = new PlayerControls();
        characterController = GetComponent<CharacterController>();
        characterMovement = GetComponent<CharacterMovement>();

        SetupStateMachine();
    }

    void Start()
    {
        jumpTimer = new CountdownTimer(characterData.maxJumpDuration);
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();

        playerControls.Player.Move.performed += context => OnMovePerformed(context);
        playerControls.Player.Move.canceled += context => OnMoveCanceled();
        playerControls.Player.Jump.performed += context => OnJumpPerformed();
        playerControls.Player.Jump.canceled += context => OnJumpCanceled();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();

        playerControls.Player.Move.performed -= context => OnMovePerformed(context);
        playerControls.Player.Move.canceled -= context => OnMoveCanceled();
        playerControls.Player.Jump.performed -= context => OnJumpPerformed();
        playerControls.Player.Jump.canceled -= context => OnJumpCanceled();
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
        return moveInputVector.magnitude == 0f && characterController.isGrounded;
    }
    bool IsMoving()
    {
        return (moveInputVector.x > 0f) || (moveInputVector.y > 0f);
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
        HandleMovement();
        HandleJumping();
        isGrounded = characterController.isGrounded;
    }

    void OnMovePerformed(InputAction.CallbackContext context) 
    {
        moveInputVector = context.ReadValue<Vector2>();
    }
    void OnMoveCanceled()
    {
        moveInputVector = Vector2.zero;
    }

    void OnJumpPerformed()
    {
        if (!jumpTimer.IsRunning && characterController.isGrounded)
        {
            jumpTimer.Start();
        }
    }

    void OnJumpCanceled()
    {
        jumpTimer.Stop();
    }

    public void HandleMovement()
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, transform.right * moveInputVector.x, characterData.turnSpeed * Mathf.Abs(moveInputVector.x) * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        Vector3 horizontalMoveVector = transform.forward * characterData.moveSpeed * moveInputVector.y;
        playerVelocity.x = horizontalMoveVector.x;
        playerVelocity.z = horizontalMoveVector.z;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleJumping()
    {
        if(jumpTimer.IsRunning)
        {
            playerVelocity.y = characterData.jumpPower;
        }
        else if(characterController.isGrounded)
        {
            playerVelocity.y = -characterData.groundingForce;
        }
        else if(!characterController.isGrounded) 
        {
            playerVelocity.y -= characterData.gravity * Time.deltaTime;
        }
    }
}
