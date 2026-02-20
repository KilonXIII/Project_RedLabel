using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
	public EnemyIdleState(EnemyAI ai, EnemyController controller) : base(ai, controller) { }

	public override void Enter() { }

	public override void Update()
	{
		float distance = Vector3.Distance(ai.transform.position, ai.player.position);
		if (distance < ai.detectRange)
		{
			ai.ChangeState(ai.chaseState);
		}
	}

	public override void Exit() { }
}