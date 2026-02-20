using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float moveSpeed = 3f;
	public float attackRange = 1.5f;
	public float detectRange = 10f;

	[HideInInspector] public Transform player;
	[HideInInspector] public SpriteRenderer spriteRenderer;
	public Animator anim;

	private EnemyBaseState currentState;

	// 상태들
	public EnemyIdleState idleState;
	public EnemyChaseState chaseState;
	public EnemyAttackState attackState;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		anim = GetComponentInChildren<Animator>();

		EnemyController controller = GetComponent<EnemyController>();

		// 상태 객체 생성
		idleState = new EnemyIdleState(this, controller);
		chaseState = new EnemyChaseState(this, controller);
		attackState = new EnemyAttackState(this, controller);
	}

	void Start()
	{
		ChangeState(idleState);
	}

	void Update()
	{
		if (currentState != null)
			currentState.Update();
	}

	public void ChangeState(EnemyBaseState newState)
	{
		if (currentState != null)
			currentState.Exit();

		currentState = newState;
		currentState.Enter();
	}
}