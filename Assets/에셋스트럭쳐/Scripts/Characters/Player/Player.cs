using UnityEngine;

public class Player : Entity
{
    #region Components & State Machine
    public PlayerInputHandler inputHandler { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public Animator animator { get; private set; }
    public MovementController movement { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine(this);

        inputHandler = GetComponent<PlayerInputHandler>();
        
        animator = GetComponent<Animator>();
        
        movement = GetComponent<MovementController>();
    }

    private void Start()
    {
        stateMachine.Initialize(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }


}