using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;

    [Header("Parameters")]
    [SerializeField] private float moveSpeed; // TODO: come from data
    [SerializeField] private float turnSpeed; // TODO: come from data

    [Header("Information")]
    [SerializeField] private IState currentState; // TODO might need a string?

    private StateMachine stateMachine;
    private PlayerControls playerControls;
    private CharacterController characterController;
    private Vector2 moveInputVector;

    void Awake()
    {
        playerControls = new PlayerControls();

        SetupStateMachine();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveSpeed = 5.0f;
        turnSpeed = 2.0f;
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

        // transitions
        At(idleState, moveState, new FuncPredicate(IsMoving));

        Any(idleState, new FuncPredicate(IsIdle));

        // initialize
        stateMachine.SetState(idleState);
    }

    void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    bool IsIdle()
    {
        return moveInputVector.magnitude == 0f;
    }
    bool IsMoving()
    {
        return moveInputVector.magnitude > 0f;
    }

    void Update()
    {
        currentState = stateMachine.GetCurrentState();
    }

    void LateUpdate()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    void OnMove(InputValue value) 
    {
        moveInputVector = value.Get<Vector2>();
    }

    public void HandleMovement()
    {
        characterController.Move(transform.forward * moveSpeed * moveInputVector.y * Time.deltaTime);

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, transform.right * moveInputVector.x, turnSpeed * Mathf.Abs(moveInputVector.x) * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void HandleJump()
    {
        // noop
        // TODO: check for grounded
        // TODO: launch with some velocity
        // TODO: implement state transition possibilities, e.g. can't go from jump to move
    }
}
