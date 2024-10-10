using UnityEngine;

public class RestState : BaseState {
    public RestState(PlayerController playerController, Animator animator, PlayerStamina playerStamina) : base(playerController, animator, playerStamina) { }
    
    public override void OnEnter() {
        //animator.CrossFade(MoveHash, crossFadeDuration);
    }

    public override void Update()
    {
        playerStamina.ModifyStamina(playerStamina.staminaData.restRegen * Time.deltaTime);
    }
}
