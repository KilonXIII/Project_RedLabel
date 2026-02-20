using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected string animBoolName; // 애니메이션 제어용

    protected float startTime; // 상태가 시작된 시간

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        player.animator.SetBool(animBoolName, true);
        // Debug.Log($"Enter State: {animBoolName}"); // 상태 확인용 로그
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate() { } // Update
    public virtual void PhysicsUpdate() { } // FixedUpdate

    // 애니메이션 종료 이벤트 수신용
    public virtual void AnimationFinishTrigger() { }
}