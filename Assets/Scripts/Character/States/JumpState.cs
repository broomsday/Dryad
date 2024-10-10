using UnityEngine;

public class JumpState : BaseState {
    public JumpState(PlayerController playerController, Animator animator, PlayerStamina playerStamina) : base(playerController, animator, playerStamina) { }
    
    public override void OnEnter() {
        //animator.CrossFade(JumpHash, crossFadeDuration);
        playerStamina.ModifyStamina(-playerStamina.staminaData.jumpCost);
    }
}
