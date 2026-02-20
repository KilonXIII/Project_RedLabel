using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Player _player;
    private Animator _anim;

    // 초기화 함수 (Player에서 호출)
    public void Initialize(Player player)
    {
        _player = player;
        _anim = GetComponent<Animator>();
    }

    #region Animation Control Methods
    // State 머신에서 접근할 래퍼(Wrapper) 함수들
    // player.Anim.SetBool 대신 player.PlayerAnim.SetBool 로 사용하게 됨

    public void SetBool(string name, bool value)
    {
        _anim.SetBool(name, value);
    }

    public void SetTrigger(string name)
    {
        _anim.SetTrigger(name);
    }

    public void SetFloat(string name, float value)
    {
        _anim.SetFloat(name, value);
    }
    
    // 필요하다면 Animator 자체를 반환하는 프로퍼티도 가능 (하지만 래핑하는게 더 좋음)
    // public Animator Animator => _anim; 
    #endregion

    #region Animation Events
    // 유니티 애니메이션 타임라인에서 호출되는 이벤트 함수는 이제 여기가 받습니다.
    
    // 예: 공격 종료 시점 등
    private void AnimationFinishTrigger()
    {
        // 본체(Player)에게 알림
        _player.OnAnimationFinished();
    }
    
    // 예: 공격 판정(Hitbox) 켜기 등 다른 이벤트도 여기서 받아서 Player로 넘김
    #endregion
}