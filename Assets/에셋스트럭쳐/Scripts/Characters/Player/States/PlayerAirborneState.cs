using UnityEngine;

public class PlayerAirborneState : State
{
    private readonly Player _player;
    private readonly PlayerStateMachine _stateMachine;

    public PlayerAirborneState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
        this._player = player;
        this._stateMachine = stateMachine;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        // 공중에서 점프 키를 또 누르면? -> 즉시 삭제(무시)해버림!
        if (_player.inputHandler.JumpTriggered) _player.inputHandler.UseJumpInput();
        
        // 1. 입력값
        Vector2 input = _player.inputHandler.MoveInput;
        
        float currentAirSpeed = _stateMachine.JumpState.IsRunJump 
            ? _player.characterData.runSpeed 
            : _player.characterData.moveSpeed;
        
        // 2. 방향전환
        _player.movement.MoveAir(input, currentAirSpeed);
        
        // 4. 공격
        if (_player.inputHandler.AttackTriggered)
        {
            _stateMachine.ChangeState(_stateMachine.AttackState);
        }

        
        // [변경점] 착지 체크 (속도 확인 및 땅 체크)
        if (_player.movement.GetCurrentVelocityY() < 0.01f && _player.movement.CheckIfGrounded())
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        
        
    }
}