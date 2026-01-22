using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerDash : MonoBehaviour
{
    [Header("Dash")]
    public float dashForce = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.5f;

    [Header("I-Frames")]
    public bool useInvulnerability = true;

    private Rigidbody2D rb;
    private Health health;
    private PlayerMovement playerMovement;

    private bool canDash = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            playerMovement.movementLocked = true;
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;

        if (useInvulnerability)
            health.isInvulnerable = true;

        TimeManager.Instance?.RegisterAction();

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDir = (mousePos - (Vector2)transform.position).normalized;

        rb.linearVelocity = Vector2.zero;               // <-- теперь правильно
        rb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        if (useInvulnerability)
            health.isInvulnerable = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        playerMovement.movementLocked = false;
    }
}
