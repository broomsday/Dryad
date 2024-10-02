using UnityEngine;

public class MoveState : BaseState {
    public MoveState(PlayerController playerController, Animator animator) : base(playerController, animator) { }
    
    public override void OnEnter() {
        //animator.CrossFade(MoveHash, crossFadeDuration);
    }
}
