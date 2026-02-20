using UnityEngine;
using System.Collections;

// ICameraPivotProvider 인터페이스 구현 유지
public class EnemyController : MonoBehaviour, ICameraPivotProvider
{
    [Header("Camera Pivot Settings")]
    [SerializeField] private Transform cameraPivot;
    // 인터페이스 요구사항 (Getter)
    public Transform CameraPivot => cameraPivot;

    [Header("Enemy Status")]
    [SerializeField] private int maxHealth = 100; // 최대 체력
    private int currentHealth;

    [Header("Visual Effects")]
    [SerializeField] private SpriteRenderer spriteRenderer; // 자식에 있는 스프라이트 렌더러 연결
    [SerializeField] private Sprite damagedSprite; // 맞았을 때 바뀔 이미지
    [SerializeField] private float damageDuration = 0.2f; // 이미지가 바뀌어 있는 시간

    private Sprite originalSprite; // 원래 모습 저장용
    private bool isDead = false;

    private void Start()
    {
        // 1. 체력 초기화
        currentHealth = maxHealth;

        // 2. 스프라이트 렌더러 자동 할당 (인스펙터에서 비워뒀을 경우)
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // 3. 원래 스프라이트 저장 (맞고 나서 돌아오기 위해)
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    // 플레이어의 공격 판정 스크립트에서 호출할 함수
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        // 체력 감소
        currentHealth -= damageAmount;
        Debug.Log($"적 피격! 남은 체력: {currentHealth}");

        // 피격 연출 (스프라이트 교체) 실행
        if (gameObject.activeInHierarchy && currentHealth > 0)
        {
            StartCoroutine(ShowDamageEffect());
        }

        // 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 일시적으로 스프라이트를 교체하는 코루틴
    private IEnumerator ShowDamageEffect()
    {
        // 1. 맞는 모습으로 변경
        if (damagedSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = damagedSprite;
        }

        // 2. 지정된 시간 대기
        yield return new WaitForSeconds(damageDuration);

        // 3. 원래 모습으로 복구 (죽지 않았을 때만)
        if (!isDead && spriteRenderer != null)
        {
            // 만약 애니메이터가 있다면 이 코드는 무시될 수 있음 (애니메이터가 우선)
            spriteRenderer.sprite = originalSprite;
        }
    }

    private void Die()
    {
        isDead = true;

        // 여기에 사망 애니메이션이나 아이템 드롭 로직 추가 가능
        Debug.Log("적 사망 처리를 시작합니다.");

        // 오브젝트 삭제
        Destroy(gameObject);
    }
}