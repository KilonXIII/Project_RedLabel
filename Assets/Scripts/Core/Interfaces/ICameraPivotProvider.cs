using UnityEngine;

// 이 인터페이스를 상속받는 모든 클래스는
// 반드시 CameraPivot이라는 Transform을 가지고 있어야 한다고 약속합니다.
public interface ICameraPivotProvider
{
    Transform CameraPivot { get; }
}