using UnityEngine;

public class IdleState : BaseState {
    public IdleState(PlayerController playerController, Animator animator) : base(playerController, animator) { }
    
    public override void OnEnter() {
        //animator.CrossFade(MoveHash, crossFadeDuration);
        // noop
    }
}
