using UnityEngine;

public abstract class BaseState : IState {
    protected readonly PlayerController playerController;
    protected readonly Animator animator;
    protected readonly PlayerStamina playerStamina;
    
    protected static readonly int IdleHash = Animator.StringToHash("Idle");
    protected static readonly int MoveHash = Animator.StringToHash("Move");
    
    protected const float crossFadeDuration = 0.1f;
    
    protected BaseState(PlayerController playerController, Animator animator, PlayerStamina playerStamina) {
        this.playerController = playerController;
        this.animator = animator;
        this.playerStamina = playerStamina;
    }
    
    public virtual void OnEnter() {
        // noop
    }
    public virtual void Update() {
        // noop
    }
    public virtual void FixedUpdate() {
        // noop
    }
    public virtual void OnExit() {
        // noop
    }
}
