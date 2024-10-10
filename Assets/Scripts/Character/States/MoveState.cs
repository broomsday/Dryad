using UnityEngine;

public class MoveState : BaseState {
    public MoveState(PlayerController playerController, Animator animator, PlayerStamina playerStamina) : base(playerController, animator, playerStamina) { }
    
    public override void OnEnter() {
        //animator.CrossFade(MoveHash, crossFadeDuration);
    }

    public override void Update()
    {
        playerStamina.ModifyStamina(-playerStamina.staminaData.moveCost * Time.deltaTime);
    }
}
