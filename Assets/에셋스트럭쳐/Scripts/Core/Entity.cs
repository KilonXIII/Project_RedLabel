using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public CharacterData characterData; // 인스펙터에서 드래그&드롭 할 곳

    // 실제 게임 중에 변하는 값
    public float CurrentHealth { get; private set; }

    protected virtual void Awake()
    {
        // 데이터가 연결되어 있다면 체력 초기화
        if (characterData != null)
        {
            CurrentHealth = characterData.maxHealth;
        }
    }

    public virtual void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        Debug.Log($"{name} took {amount} damage. Current HP: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{name} Died!");
        // 나중에 사망 애니메이션이나 Destroy 로직 추가
    }

    // 애니메이션 종료 이벤트 수신용 (StateMachineBehaviour에서 호출)
    public virtual void OnAnimationFinished(string eventName = "")
    {
        // 자식 클래스(Player, Enemy 등)에서 오버라이드하여 구체적인 로직 구현
        // 예: 공격 종료 후 Idle 상태로 복귀, 피격 애니메이션 종료 후 상태 초기화 등
    }
}