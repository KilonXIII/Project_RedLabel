using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private float idleTimer = 0f;
	private float idleDuration = 2f;

	public EnemyIdleState(EnemyAI ai, EnemyController controller) : base(ai, controller) { }

	public override void Enter() 
	{
		if (ai.anim != null)
		{
			ai.anim.SetBool("isMoving", false);//정시 상태
		}
		else
		{
            Debug.LogWarning($"{ai.gameObject.name}에 Animator가 없습니다! 인스펙터를 확인하세요.");
		}
		
		idleTimer = 0f; // 상태 진입 시 타이머 초기화
		if (ai.anim != null) ai.anim.SetBool("isMoving", false);
	}


	public override void Update()
	{
		float distance = Vector3.Distance(ai.transform.position, ai.player.position);
		if (distance < ai.detectRange)
		{
			ai.ChangeState(ai.chaseState);
			return;
		}

		idleTimer += Time.deltaTime;
		if (idleTimer >= idleDuration)
		{
		
			ai.ChangeState(ai.patrolState); // 순찰 시작!
		}
	}

	public override void Exit() { }
}