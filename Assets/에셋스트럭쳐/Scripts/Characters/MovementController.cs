using UnityEngine;

public class MovementController : MonoBehaviour
{
    // [컴포넌트]
    private Rigidbody RB;
    
    // [변수]
    private Vector3 workspace; 
    public int FacingDirection { get; private set; } = 1; 

    [Header("Checks")]
    public Transform groundCheck; 
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround; 
    
    [Header("Air Sliding Settings")]
    public float stopFriction = 5f;
    
    public float diveSpeed = 15f;
    
    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    // --- [핵심 기능] ---

    // 1. 이동 (걷기, 공중 이동 등)
    public void Move(Vector2 input, float speed)
    {
        // 벨트스크롤: X, Y 입력을 X, Z 이동으로 변환
        workspace.Set(input.x * speed, RB.linearVelocity.y, input.y * speed);
        RB.linearVelocity = workspace;

        Flip(input);
    }

    public void Flip(Vector2 input)
    {
        // 방향 전환 체크
        if (input.x != 0)
        {
            CheckIfShouldFlip((int)Mathf.Sign(input.x));
        }
    }

    // 2. 점프 (위로 힘 가하기)
    public void Jump(float jumpForce)
    {
        workspace.Set(RB.linearVelocity.x, jumpForce, RB.linearVelocity.z);
        RB.linearVelocity = workspace;
    }
    
    public void MoveAir(Vector2 input, float speed)
    {
        Vector3 currentVelocity = RB.linearVelocity;

        // 1. 입력이 있을 때: 가속도 없이 즉시 목표 속도로 이동
        if (input.magnitude > 0)
        {
            // "캐릭터의 이동속도대로 움직임" -> 속도 덮어쓰기
            workspace.Set(input.x * speed, currentVelocity.y, input.y * speed);
            RB.linearVelocity = workspace;

            // 방향 전환
            if (input.x != 0) CheckIfShouldFlip((int)Mathf.Sign(input.x));
        }
        // 2. 입력이 없을 때: 미끄러지듯 멈춤 (Sliding Stop)
        else
        {
            // 현재 속도(current)에서 0(zero)까지 stopFriction 속도로 서서히 줄어듦 (Lerp)
            float x = Mathf.Lerp(currentVelocity.x, 0f, Time.deltaTime * stopFriction);
            float z = Mathf.Lerp(currentVelocity.z, 0f, Time.deltaTime * stopFriction);

            workspace.Set(x, currentVelocity.y, z); // Y축(중력)은 건드리지 않음
            RB.linearVelocity = workspace;
        }
    }
    
    public void Dive()
    {
        // X, Z는 멈추고(혹은 유지하고), Y축만 강하게 내리꽂음
        workspace.Set(0, -diveSpeed, 0); 
        RB.linearVelocity = workspace;
    }

    // 3. 즉시 정지 (공격 시 미끄러짐 방지)
    public void StopImmediately()
    {
        workspace.Set(0, RB.linearVelocity.y, 0); // 중력(Y)은 유지해야 함
        RB.linearVelocity = workspace;
    }

    // --- [Helper Methods] ---

    public bool CheckIfGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public float GetCurrentVelocityY()
    {
        return RB.linearVelocity.y;
    }

    private void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}