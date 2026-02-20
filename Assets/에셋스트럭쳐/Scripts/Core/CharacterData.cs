using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Data/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public float runSpeed = 20f;

    [Header("Combat")]
    public float attackDamage = 10f;
    public float attackRadius = 1.5f; // 공격 범위 (반지름)
    public float comboWindow = 0.8f;  // 이 시간 안에 다시 눌러야 콤보가 이어짐
    public float attackKnockback = 3f; // 적을 밀어내는 힘
    
    
}