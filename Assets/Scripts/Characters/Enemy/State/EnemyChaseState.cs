using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
	public EnemyChaseState(EnemyAI ai, EnemyController controller) : base(ai, controller) { }

	public override void Enter() { }

	public override void Update()
	{
		if (ai.player == null) return;

		float distance = Vector3.Distance(ai.transform.position, ai.player.position);

		if (distance > ai.detectRange)
		{
			ai.ChangeState(ai.idleState);
			return;
		}

		if (distance <= ai.attackRange)
		{
			ai.ChangeState(ai.attackState);
			return;
		}

		Vector3 direction = (ai.player.position - ai.transform.position).normalized;
		// 3D 기반 이동 (Z축 활용)
		Vector3 moveVector = new Vector3(direction.x, 0f, direction.z);
		ai.transform.Translate(moveVector * ai.moveSpeed * Time.deltaTime, Space.World);

		if (ai.spriteRenderer != null)
			ai.spriteRenderer.flipX = (direction.x < 0);
	}

	public override void Exit() { }
}