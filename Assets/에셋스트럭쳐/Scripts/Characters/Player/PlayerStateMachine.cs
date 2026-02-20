public class PlayerStateMachine : StateMachine
{
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirborneState AirborneState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }
    
    public PlayerStateMachine(Player player) : base(player)
    {
        // 'this'는 StateMachine 자신을 의미하므로 상태 생성자에 넘겨주기 편합니다.
        IdleState = new PlayerIdleState(player, this);
        MoveState = new PlayerMoveState(player, this);
        JumpState = new PlayerJumpState(player, this);
        AirborneState = new PlayerAirborneState(player, this);
        RunState = new PlayerRunState(player, this);
        AttackState = new PlayerAttackState(player, this, "Attack");
    }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
        PreviousState = null;
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();
    }
}