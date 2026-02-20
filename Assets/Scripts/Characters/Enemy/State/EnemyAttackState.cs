using UnityEngine;
using System.Collections;

public class EnemyAttackState : EnemyBaseState
{
	public EnemyAttackState(EnemyAI ai, EnemyController controller) : base(ai, controller) { }

	public override void Enter()
	{
		// 공격 상태에 진입하면 코루틴 실행
		ai.StartCoroutine(AttackRoutine());
	}

	public override void Update()
	{
		// 공격 중에는 이동하지 않으므로 비워둡니다.
	}

	public override void Exit()
	{
		// 공격 상태를 나갈 때 필요한 정리 작업 (필요 시)
	}

	private IEnumerator AttackRoutine()
	{
		// 1. 공격 애니메이션 실행
		if (ai.anim != null) ai.anim.SetTrigger("Attack");

		Debug.Log("적 공격 중...");

		// 2. 애니메이션 길이에 맞춰 대기 (예: 1초)
		yield return new WaitForSeconds(1f);

		// 3. 다시 추격 상태로 복귀
		ai.ChangeState(ai.chaseState);
	}
}