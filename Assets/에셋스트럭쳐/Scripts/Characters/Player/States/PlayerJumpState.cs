using UnityEngine;

public class PlayerJumpState : State
{
    private readonly Player _player;
    private readonly PlayerStateMachine _stateMachine;

    public bool IsRunJump { get; private set; }
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
        this._player = player;
        this._stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        
        if (_stateMachine.PreviousState is PlayerRunState) IsRunJump = true;
        else IsRunJump = false;
        
        _player.movement.Jump(_player.characterData.jumpForce);
        _player.inputHandler.UseJumpInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _stateMachine.ChangeState(_stateMachine.AirborneState);
    }
}