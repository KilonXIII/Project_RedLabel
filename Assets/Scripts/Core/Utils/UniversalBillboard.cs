using UnityEngine;

// 플레이어와 적 모두에게 사용할 수 있는 범용 빌보드 스크립트
public class UniversalBillboard : MonoBehaviour
{
    // 구체적인 클래스(PlayerController 등)가 아닌 인터페이스를 참조합니다.
    private ICameraPivotProvider pivotProvider;

    void Start()
    {
        // 부모 오브젝트에서 ICameraPivotProvider를 구현한 컴포넌트를 찾습니다.
        // PlayerController든 EnemyController든 상관없이 찾아냅니다.
        pivotProvider = GetComponentInParent<ICameraPivotProvider>();

        if (pivotProvider == null)
        {
            Debug.LogError($"[{gameObject.name}] 부모 오브젝트에서 ICameraPivotProvider를 찾을 수 없습니다!");
        }
    }

    void LateUpdate()
    {
        if (pivotProvider != null)
        {
            // 인터페이스를 통해 접근하므로 대상이 누구인지 몰라도 회전값을 가져올 수 있습니다.
            transform.rotation = pivotProvider.CameraPivot.rotation;
        }
    }
}