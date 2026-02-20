using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public enum EnemyState { Idle, Chase, Attack, Hit }
    public EnemyState currentState = EnemyState.Idle;
    
    public float moveSpeed = 3f; // 움직이는 속도임
    public float attackRange = 1.5f; //공격 범위임
    public float detectRange = 10f; //인식범위 거리 한번 테스트해봄

    private Transform player;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //플레이어 오브젝트를 찾아서 추적 대상으로 설정
    }

    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 현재 상태에 따른 행동 실행
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleBehavior();
                break;
            case EnemyState.Chase:
                ChaseBehavior();
                break;
            case EnemyState.Attack:
                break;
        }


    }

	// 1. 대기 상태 로직
	void IdleBehavior()
	{
		float distance = Vector2.Distance(transform.position, player.position);
		if (distance < detectRange)
		{
			currentState = EnemyState.Chase; // 플레이어가 근처에 오면 추격 시작
		}
	}

	// 2. 추격 및 Y축 정렬 로직 (이전의 이동 로직 통합)
	void ChaseBehavior()
	{
		if (player == null) return;

		float distance = Vector2.Distance(transform.position, player.position);

		if (distance > detectRange)
		{
			currentState = EnemyState.Idle;
			return; // 아래 추격 로직을 실행하지 않고 나갑니다.
		}

		if (distance <= attackRange)
		{
			currentState = EnemyState.Attack;
			StartCoroutine(AttackRoutine());
			return;
		}

		// 플레이어 방향으로 이동 방향 계산
		Vector3 direction = (player.position - transform.position).normalized;

		// 벨트스크롤 특유의 이동 (X, Y축 동시 이동 가능)
		Vector3 moveVector = new Vector3(direction.x, 0f, direction.z);
		transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);

		if (spriteRenderer != null)
		{
			// 플레이어가 왼쪽에 있으면 flipX를 true로, 오른쪽에 있으면 false로
			spriteRenderer.flipX = (direction.x < 0);
		}
	}

	IEnumerator AttackRoutine()
	{
		Debug.Log("적 공격 시작!");

		// 여기에 나중에 애니메이션 실행 코드를 넣으세요
		// animator.SetTrigger("Attack");

		// 공격 동작 시간 동안 대기 (예: 0.8초)
		yield return new WaitForSeconds(0.8f);

		Debug.Log("적 공격 종료! 다시 추격합니다.");

		// 다시 추격 상태로 복귀
		currentState = EnemyState.Chase;
	}

	void PerformAttack()
	{
		Debug.Log("공격!");
		// 여기에 애니메이터 컨트롤러 연결 (예: animator.SetTrigger("Attack"))
		// 공격 애니메이션이 끝나면 다시 Idle이나 Chase로 돌아오는 로직이 필요합니다.
	}
}
