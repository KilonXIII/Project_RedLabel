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
		ai.anim.SetTrigger("Attack"); // 공격 애니메이션 트리거
		ai.anim.SetBool("isMoving", false); // 공격 중엔 발 멈춤

		// 1. 공격 애니메이션의 휘두르는 타이밍까지 대기 (임시 0.5초)
		yield return new WaitForSeconds(0.5f);

		// 2. 실제 데미지 판정 로직 (나중에 여기에 작성)
		Debug.Log("적 타격 판정 발생!");

		// 3. 전체 애니메이션이 끝날 때까지 대기 (임시 0.5초)
		yield return new WaitForSeconds(0.5f);

		// 4. 추격 상태로 복귀
		ai.ChangeState(ai.chaseState);
	}
}