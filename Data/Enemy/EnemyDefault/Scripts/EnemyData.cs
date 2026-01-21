using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float maxHealth = 3f;
    public float moveSpeed = 2.5f;

    [Header("Melee")]
    public float contactDamage = 1f;
    public float damageCooldown = 0.8f;
}
