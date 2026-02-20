using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions
{
    // C# Action을 사용하여 가벼운 이벤트 전달
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction JumpEvent;
    public event UnityAction JumpCanceledEvent;
    public event UnityAction AttackEvent;

    private PlayerInputActions _inputActions;

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
        }
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    // New Input System 인터페이스 구현부
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // 버튼을 눌렀을 때 실행할 로직
        if (context.performed)
        {
            // 예: ParryEvent?.Invoke();
            Debug.Log("점프 키 눌림");
        }
        if (context.phase == InputActionPhase.Performed)
            JumpEvent?.Invoke();
        else if (context.phase == InputActionPhase.Canceled)
            JumpCanceledEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) 
        {
            AttackEvent?.Invoke();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // 예: ParryEvent?.Invoke();
            Debug.Log("Dash Action Performed");
        }
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        // 패링 버튼을 눌렀을 때 실행할 로직
        if (context.performed)
        {
            // 예: ParryEvent?.Invoke();
            Debug.Log("패링 키 눌림");
        }
    }
}