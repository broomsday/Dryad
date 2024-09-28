using UnityEngine;

public abstract class BaseState : IState {
    protected readonly PlayerController playerController;
    protected readonly Animator animator;
    
    protected static readonly int IdleHash = Animator.StringToHash("Idle");
    protected static readonly int MoveHash = Animator.StringToHash("Move");
    
    protected const float crossFadeDuration = 0.1f;
    
    protected BaseState(PlayerController playerController, Animator animator) {
        this.playerController = playerController;
        this.animator = animator;
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
