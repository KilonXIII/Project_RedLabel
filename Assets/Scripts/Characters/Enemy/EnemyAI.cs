using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float moveSpeed = 3f;
	public float attackRange = 1.5f;
	public float detectRange = 10f;

	[HideInInspector] public Transform player;
	[HideInInspector] public SpriteRenderer spriteRenderer;
	[HideInInspector] public Animator anim;

	private EnemyBaseState currentState;

	// 상태들
	public EnemyIdleState idleState;
	public EnemyChaseState chaseState;
	public EnemyAttackState attackState;
	public EnemyPatrolState patrolState;

	void Awake()
	{
		// 1. 가장 먼저 플레이어를 찾습니다.
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		if (playerObj != null) player = playerObj.transform;

		// 2. 중요: Animator를 찾는 방식을 더 확실하게 합니다.
		anim = GetComponent<Animator>(); // 본체에 있는지 확인
		if (anim == null) anim = GetComponentInChildren<Animator>(); // 자식에게 있는지 확인

		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		// 3. 컨트롤러 연결
		EnemyController controller = GetComponent<EnemyController>();

		// 4. 상태 객체 생성
		idleState = new EnemyIdleState(this, controller);
		chaseState = new EnemyChaseState(this, controller);
		attackState = new EnemyAttackState(this, controller);
		patrolState = new EnemyPatrolState(this, controller);
		// hitState가 있다면 추가: hitState = new EnemyHitState(this, controller);
	}

	void Start()
	{
	// 모든 준비(Awake)가 끝난 뒤에 첫 상태를 시작
		if (idleState != null)
		{
			ChangeState(idleState);

		}
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