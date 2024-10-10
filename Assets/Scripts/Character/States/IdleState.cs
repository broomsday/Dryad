using UnityEngine;

public class IdleState : BaseState {
    public IdleState(PlayerController playerController, Animator animator, PlayerStamina playerStamina) : base(playerController, animator, playerStamina) { }
    
    public override void OnEnter() {
        //animator.CrossFade(MoveHash, crossFadeDuration);
    }

    public override void Update()
    {
        playerStamina.ModifyStamina(playerStamina.staminaData.idleRegen * Time.deltaTime);
    }
}
