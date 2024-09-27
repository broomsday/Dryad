using UnityEngine;

public class MoveState : BaseState {
    public MoveState(PlayerController player, Animator animator) : base(player, animator) { }
    
    public override void OnEnter() {
        //animator.CrossFade(LocomotionHash, crossFadeDuration);
        // noop
    }
    
    public override void FixedUpdate() {
        player.HandleMovement();
    }
}
