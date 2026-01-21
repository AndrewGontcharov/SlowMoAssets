using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    public float maxHealth = 5f;
    public bool isInvulnerable;

    private float currentHealth;
    public static event Action<GameObject> OnEnemyKilled;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isInvulnerable) return;

        currentHealth -= amount;

        if (currentHealth <= 0f)
            Die();
    }

    private void Die()
    {
        if (CompareTag("Player"))
        {
            Debug.Log("PLAYER DEAD");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            OnEnemyKilled?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }
}
