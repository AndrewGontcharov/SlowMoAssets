using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    public float maxHealth = 5f;
    public bool isInvulnerable;

    private float currentHealth;
    private bool isDead = false;

    public static event Action<GameObject> OnEnemyKilled;
    public static event Action OnPlayerDead;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        isInvulnerable = false;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;
        if (isInvulnerable) return;

        currentHealth -= amount;

        if (currentHealth <= 0f)
            Die();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (CompareTag("Player"))
        {
            OnPlayerDead?.Invoke();
            GameManager.Instance.GameOver();
        }
        else
        {
            OnEnemyKilled?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }
}
