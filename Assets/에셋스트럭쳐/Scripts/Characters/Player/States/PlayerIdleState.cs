using UnityEngine;

public class PlayerIdleState : State
{
    private readonly Player _player;
    private readonly PlayerStateMachine _stateMachine;
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
        this._player = player;
        this._stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        
        // [변경점 1] 직접 속도를 0으로 셋팅하던 것을 '정지 기능' 호출로 변경
        // 이 함수가 X, Z 속도만 0으로 만들고 중력(Y)은 유지해 줍니다.
        _player.movement.StopImmediately();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 1. 이동 입력이 있으면 MoveState로
        if (_player.inputHandler.MoveInput != Vector2.zero)
        {
            _stateMachine.ChangeState(_stateMachine.MoveState);
        }

        // 2. 점프 (땅 체크를 Movement 컴포넌트에게 물어봄)
        if (_player.inputHandler.JumpTriggered && _player.movement.CheckIfGrounded())
        {
            _stateMachine.ChangeState(_stateMachine.JumpState);
        }

        // 3. 달리기
        if (_player.inputHandler.runTriggered)
        {
            _stateMachine.ChangeState(_stateMachine.RunState);
        }
        
        // 4. 공격
        if (_player.inputHandler.AttackTriggered)
        {
            _stateMachine.ChangeState(_stateMachine.AttackState);
        }
    }
}