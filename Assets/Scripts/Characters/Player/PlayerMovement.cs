using UnityEngine;
// using UnityEngine.InputSystem; // 더 이상 필요 없음!
using System;

public class PlayerMovement : MonoBehaviour, ICameraPivotProvider
{
    [Header("설정")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform cameraPivot;

    [Header("입력 (InputReader 연결)")]
    [SerializeField] private InputReader inputReader; // InputActionReference 대신 이거 사용

    // 외부 전달 이벤트
    public event Action<bool> OnLookDirectionChanged;
    public event Action<Vector2> OnMove;

    public bool IsFacingRight { get; private set; } = true;
    public Transform CameraPivot => cameraPivot;

    private float _zCompensationFactor;
    private Vector2 _currentMoveInput; // 현재 입력값을 저장할 변수

    private void Awake() => CalculateCompensation();

    private void OnEnable()
    {
        // InputReader의 이벤트를 구독합니다.
        if (inputReader != null)
        {
            inputReader.MoveEvent += HandleMoveInput;
        }
    }

    private void OnDisable()
    {
        // 구독 해제 (필수)
        if (inputReader != null)
        {
            inputReader.MoveEvent -= HandleMoveInput;
        }
    }

    // InputReader가 값을 보낼 때 실행되는 함수
    private void HandleMoveInput(Vector2 input)
    {
        _currentMoveInput = input;

        // 입력이 들어올 때 바로 애니메이션 쪽으로도 신호 전달
        OnMove?.Invoke(_currentMoveInput);
    }

    private void CalculateCompensation()
    {
        if (cameraPivot == null) return;
        float cameraX = cameraPivot.rotation.eulerAngles.x;
        float angleInRadians = cameraX * Mathf.Deg2Rad;
        float sinValue = Mathf.Sin(angleInRadians);
        _zCompensationFactor = 1f / Mathf.Max(sinValue, 0.01f);
    }

    private void Update()
    {
        // _currentMoveInput 변수에 저장된 값을 사용 (매 프레임 ReadValue 할 필요 없음)
        Vector2 input = _currentMoveInput;

        // 1. 방향 전환 로직
        if (input.x != 0)
        {
            bool lookRight = input.x > 0;
            if (lookRight != IsFacingRight)
            {
                IsFacingRight = lookRight;
                OnLookDirectionChanged?.Invoke(IsFacingRight);
            }
        }

        // 2. 이동 로직
        if (input.sqrMagnitude > 1f) input.Normalize();

        Vector3 moveDirection = new Vector3(input.x, 0f, input.y * _zCompensationFactor);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}