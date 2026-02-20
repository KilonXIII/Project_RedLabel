using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Entity _entity;
    private Animator _animator;

    protected void Initialize(Entity entity)
    {
        _entity = entity;
        _animator = GetComponent<Animator>();
    }
    
    // 1. 애니메이션 재생 위임
    public void PlayAnimation(int animHash)
    {
        _animator.Play(animHash);
    }
    
    // 2. 애니메이션 종료 여부 확인 위임
    // finishTime 파라미터를 두어, 1.0f(완료) 뿐만 아니라 0.8f(선입력 타이밍) 등도 유연하게 체크할 수 있습니다.
    public bool IsAnimationFinished(string targetTag, float finishTime = 1.0f)
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (!_animator.IsInTransition(0) && stateInfo.IsTag(targetTag))
        {
            if (stateInfo.normalizedTime >= finishTime)
            {
                return true;
            }
        }
        return false;
    }
}
