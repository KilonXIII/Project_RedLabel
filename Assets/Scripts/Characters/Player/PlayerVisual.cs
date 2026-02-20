using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private Transform modelTransform; // 실제 회전할 모델(스프라이트)

    // 내부 참조 변수 (인스펙터 할당 X, 코드로 자동 할당)
    private PlayerMovement _movement;
    private PlayerAttack _attack;

    private void Awake()
    {
        // 1. 같은 오브젝트에 있는 PlayerController(파사드)를 찾습니다.
        var controller = GetComponent<PlayerController>();

        // 2. 파사드를 통해 각 모듈을 가져옵니다. (안전성 확보)
        _movement = controller.Movement;
        _attack = controller.Attack;
    }

    private void OnEnable()
    {
        // 3. 이동 관련 이벤트 구독
        if (_movement != null)
        {
            _movement.OnLookDirectionChanged += HandleDirectionChange;
            _movement.OnMove += HandleMovementAnimation;
        }

        // 4. 공격 관련 이벤트 구독 (공격 스크립트에 있던 이벤트 활용)
        if (_attack != null)
        {
            _attack.OnAttackPerformed += HandleAttackAnimation;
        }
    }

    private void OnDisable()
    {
        // 구독 해제 (필수)
        if (_movement != null)
        {
            _movement.OnLookDirectionChanged -= HandleDirectionChange;
            _movement.OnMove -= HandleMovementAnimation;
        }

        if (_attack != null)
        {
            _attack.OnAttackPerformed -= HandleAttackAnimation;
        }
    }

    // --- 핸들러 구현부 ---

    // 방향 전환 (Scale 반전)
    private void HandleDirectionChange(bool isFacingRight)
    {
        Vector3 currentScale = modelTransform.localScale;
        currentScale.x = isFacingRight ? Mathf.Abs(currentScale.x) : -Mathf.Abs(currentScale.x);
        modelTransform.localScale = currentScale;
    }

    // 이동 애니메이션 처리
    private void HandleMovementAnimation(Vector2 input)
    {
        // 예시: animator.SetBool("IsRunning", input.sqrMagnitude > 0.01f);
        // Debug.Log($"애니메이션: 이동 신호 {input}");
    }

    // 공격 애니메이션 처리
    private void HandleAttackAnimation()
    {
        // 예시: animator.SetTrigger("Attack");
        // Debug.Log("애니메이션: 공격!");
    }
}