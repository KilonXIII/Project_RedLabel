using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
	public EnemyIdleState(EnemyAI ai, EnemyController controller) : base(ai, controller) { }

	public override void Enter() 
	{
		ai.anim.SetBool("isMoving", false);//정시 상태

	}


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