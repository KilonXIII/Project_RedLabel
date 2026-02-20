using UnityEngine;

public class PlayerMoveState : State
{
    private readonly Player _player;
    private readonly PlayerStateMachine _stateMachine;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
        this._player = player;
        this._stateMachine = stateMachine;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 1. 입력값
        Vector2 input = _player.inputHandler.MoveInput;

        // 2. [변경점] Movement 컴포넌트를 통해 이동 명령
        _player.movement.Move(input, _player.characterData.moveSpeed);

        // 3. 상태 전이
        if (input == Vector2.zero)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

        // [점프] CheckIfGrounded도 Movement에 있는 것을 사용
        if (_player.inputHandler.JumpTriggered && _player.movement.CheckIfGrounded())
        {
            _stateMachine.ChangeState(_stateMachine.JumpState);
        }

        // [달리기]
        if (_player.inputHandler.runTriggered)
        {
            _stateMachine.ChangeState(_stateMachine.RunState);
        }
    }
}