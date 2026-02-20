using UnityEngine;

public abstract class EnemyBaseState
{
	protected EnemyAI ai;
	protected EnemyController controller;

	public EnemyBaseState(EnemyAI ai, EnemyController controller)
	{
		this.ai = ai;
		this.controller = controller;
	}

	public abstract void Enter();   // 상태 시작 시 실행
	public abstract void Update();  // 매 프레임 실행
	public abstract void Exit();    // 상태 종료 시 실행
}