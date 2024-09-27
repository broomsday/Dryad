using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;

    StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        var moveState = new MoveState(this, animator);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleMovement()
    {
        // noop
    }
}
