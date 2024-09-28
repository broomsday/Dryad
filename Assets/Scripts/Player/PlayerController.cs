using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;

    private StateMachine stateMachine;
    private PlayerControls playerControls;
    private Vector2 moveInputVector;

    void Awake()
    {
        playerControls = new PlayerControls();

        SetupStateMachine();
    }

    void Start()
    {
        // noop
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
        // noop
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
        Debug.Log("Movement should happen");
    }
}
