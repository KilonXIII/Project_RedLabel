using UnityEngine;

// 이 스크립트는 플레이어 오브젝트의 최상위(Root)에 붙습니다.
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerVisual))]
public class PlayerController : MonoBehaviour
{
    // 외부에서 접근 가능한 읽기 전용 프로퍼티
    public PlayerMovement Movement { get; private set; }
    public PlayerAttack Attack { get; private set; }
    public PlayerVisual Visual { get; private set; }

    private void Awake()
    {
        // 내 오브젝트에 붙어있는 컴포넌트들을 가져와서 캐싱합니다.
        Movement = GetComponent<PlayerMovement>();
        Attack = GetComponent<PlayerAttack>();
        Visual = GetComponent<PlayerVisual>();
    }

    // (선택 사항) 플레이어 조작을 전체적으로 켜고 끌 때 사용 (예: 컷신, 대화 중)
    public void SetControlActive(bool active)
    {
        if (Movement != null) Movement.enabled = active;
        if (Attack != null) Attack.enabled = active;
        // Visual은 보통 끄지 않습니다 (애니메이션은 계속 나와야 하니까요)
    }
}