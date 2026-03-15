using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
	private Vector3 targetPosition;
	private float patrolTime = 2f; // 한 지점까지 가는데 걸리는 시간 제한
	private float timer = 0f;

	public EnemyPatrolState(EnemyAI ai, EnemyController controller) : base(ai, controller) { }

	public override void Enter()
	{
		timer = 0f;
		SetRandomTarget(); // 시작할 때 목적지 설정
		if (ai.anim != null) ai.anim.SetBool("isMoving", true);
	}

	public override void Update()
	{
		// 1. 플레이어 발견 시 바로 추격
		float distanceToPlayer = Vector3.Distance(ai.transform.position, ai.player.position);
		if (distanceToPlayer < ai.detectRange)
		{
			ai.ChangeState(ai.chaseState);
			return;
		}

		// 2. 목적지로 이동
		Vector3 direction = (targetPosition - ai.transform.position).normalized;
		ai.transform.Translate(new Vector3(direction.x, 0, direction.z) * (ai.moveSpeed * 0.5f) * Time.deltaTime, Space.World);

		// 스프라이트 방향 전환
		if (ai.spriteRenderer != null && direction.x != 0)
			ai.spriteRenderer.flipX = (direction.x < 0);

		// 3. 목적지에 도착했거나 시간이 너무 오래 지났으면 다시 대기(Idle)로
		timer += Time.deltaTime;
		if (Vector3.Distance(ai.transform.position, targetPosition) < 0.2f || timer > patrolTime)
		{
			ai.ChangeState(ai.idleState);
		}
	}

	public override void Exit() { }

	private void SetRandomTarget()
	{
		// 현재 위치 기준으로 반경 3~5 유닛 정도 랜덤 지점 설정
		float randomX = Random.Range(-3f, 3f);
		float randomZ = Random.Range(-2f, 2f);
		targetPosition = ai.transform.position + new Vector3(randomX, 0, randomZ);
	}
}