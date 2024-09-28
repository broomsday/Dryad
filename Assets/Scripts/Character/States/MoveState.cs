using UnityEngine;

public class MoveState : BaseState {
    public MoveState(PlayerController playerController, Animator animator) : base(playerController, animator) { }
    
    public override void OnEnter() {
        Debug.Log("Entering move state");
        //animator.CrossFade(MoveHash, crossFadeDuration);
        // noop
    }
    
    public override void FixedUpdate() {
        playerController.HandleMovement();
    }
}
