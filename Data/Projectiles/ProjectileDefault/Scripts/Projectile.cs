using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Data")]
    public ProjectileData data;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // ����� ����, ���� ������� ���� (top-down ��������)
        rb.linearVelocity = transform.up * data.speed;

        // ������������ �� �������
        Destroy(gameObject, data.lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �� ������� ������
        if (other.CompareTag("Player")) return;

        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(data.damage);
            Destroy(gameObject);
        }
    }
}
