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
		// 1. ai나 ai.anim이 Null인지 확인
		if (ai == null) yield break;

		if (ai.anim != null)
		{
			ai.anim.SetTrigger("Attack");
			ai.anim.SetBool("isMoving", false);
		}
		else
		{
			Debug.LogWarning("EnemyAttackState: Animator가 없습니다!");
		}

		Debug.Log("적 공격 시작");

		// 2. 공격 동작 대기 (0.5초)
		yield return new WaitForSeconds(0.5f);

		// 3. 타격 판정 시점에서 플레이어 존재 확인 (26번 줄 근처 예상 지점)
		if (ai.player != null)
		{
			float distance = Vector3.Distance(ai.transform.position, ai.player.position);
			if (distance <= ai.attackRange)
			{
				Debug.Log("플레이어 타격!");
			}
		}

		// 4. 나머지 애니메이션 시간 대기
		yield return new WaitForSeconds(0.5f);

		// 5. 다시 추격 상태로 변경
		ai.ChangeState(ai.chaseState);
	}
}