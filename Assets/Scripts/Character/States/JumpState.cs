using UnityEngine;

public class JumpState : BaseState {
    public JumpState(PlayerController playerController, Animator animator) : base(playerController, animator) { }
    
    public override void OnEnter() {
        //animator.CrossFade(JumpHash, crossFadeDuration);
    }
}
