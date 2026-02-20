using UnityEngine;

public class PlayerAttackState : State
{
    private readonly Player _player;
    private readonly PlayerStateMachine _stateMachine;
    
    // 실행할 애니메이션의 상태(State) 이름 해시값
    private readonly int _animHash;
    
    // 애니메이터 상태가 'Attack' 태그인지 확인하기 위한 안전장치
    private const string TargetTag = "Attack";
    
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animName) 
        : base(player, stateMachine)
    {
        this._player = player;
        this._stateMachine = stateMachine;
        _animHash = Animator.StringToHash(animName);
    }

    public override void Enter()
    {
        base.Enter();
        
        _player.animator.Play(_animHash);
        
        _player.inputHandler.UseAttackInput(); // 입력 소비
        
        _player.movement.StopImmediately();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        AnimatorStateInfo stateInfo = _player.animator.GetCurrentAnimatorStateInfo(0);
// 컨트롤러에게 "Attack 태그 애니메이션이 100% 다 끝났니?" 라고 물어봅니다.
        if (!_player.animator.IsInTransition(0) && stateInfo.shortNameHash == _animHash)
        {
            if (stateInfo.normalizedTime >= 0.95f)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
    }
}