using UnityEngine;
using System.Collections;

public class EnemyHitState : EnemyBaseState
{
	public EnemyHitState(EnemyAI ai, EnemyController controller) : base(ai, controller) { }

	public override void Enter()
	{
		ai.anim.SetTrigger("Hit"); // 피격 애니메이션 트리거
		ai.StartCoroutine(HitWaitRoutine());
	}

	private IEnumerator HitWaitRoutine()
	{
		// 피격 시 경직 시간 (예: 0.3초)
		yield return new WaitForSeconds(0.3f);

		// 경직이 풀리면 다시 추격 상태로
		ai.ChangeState(ai.chaseState);
	}

	public override void Update() { }
	public override void Exit() { }
}