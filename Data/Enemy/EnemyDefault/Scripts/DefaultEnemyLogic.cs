using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class DefaultEnemy : MonoBehaviour
{
    public EnemyData data;

    private Transform BodyPlayer;
    private Rigidbody2D rb;
    private Health health;

    private float lastDamageTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        BodyPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        health.maxHealth = data.maxHealth;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;


        Vector2 dir = ((Vector2)BodyPlayer.position - rb.position).normalized;
        rb.linearVelocity = dir * data.moveSpeed;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        if (Time.time >= lastDamageTime + data.damageCooldown)
        {
            collision.collider
                .GetComponent<IDamageable>()
                ?.TakeDamage(data.contactDamage);

            lastDamageTime = Time.time;
        }
    }
}
