using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;


public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;
    [SerializeField] MoveData moveData;


    [Header("Information")]
    [SerializeField] private string currentStateName;

    private StateMachine stateMachine;
    private PlayerControls playerControls;
    private CharacterController characterController;
    private CharacterMovement characterMovement;
    private PlayerInteraction playerInteraction;
    private Vector2 moveInputVector;
    CountdownTimer jumpTimer;

    void Awake()
    {
        playerControls = new PlayerControls();
        characterController = GetComponent<CharacterController>();
        characterMovement = GetComponent<CharacterMovement>();
        playerInteraction = GetComponent<PlayerInteraction>();

        SetupStateMachine();
    }

    void Start()
    {
        jumpTimer = new CountdownTimer(moveData.maxJumpDuration);
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();

        playerControls.Player.Move.performed += context => OnMovePerformed(context);
        playerControls.Player.Move.canceled += context => OnMoveCanceled();
        playerControls.Player.Jump.performed += context => OnJumpPerformed();
        playerControls.Player.Jump.canceled += context => OnJumpCanceled();
        playerControls.Player.Interact.performed += context => OnInteractPerformed();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();

        playerControls.Player.Move.performed -= context => OnMovePerformed(context);
        playerControls.Player.Move.canceled -= context => OnMoveCanceled();
        playerControls.Player.Jump.performed -= context => OnJumpPerformed();
        playerControls.Player.Jump.canceled -= context => OnJumpCanceled();
        playerControls.Player.Interact.performed -= context => OnInteractPerformed();
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
        characterMovement.HandleMovement(moveInputVector);
        characterMovement.HandleJumping(jumpTimer);
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

    void OnInteractPerformed()
    {
        playerInteraction.Interact();
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
}
