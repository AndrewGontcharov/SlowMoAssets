using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D BodyPlayer;
    public float Speed = 200f;

    private float horizontal;
    private float vertical;
    private Vector2 movement;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        movement = new Vector2(horizontal, vertical).normalized;
        BodyPlayer.linearVelocity = movement * Speed;

        // Если игрок движется — регистрируем действие
        if (movement.magnitude > 0.1f)
            TimeManager.Instance.RegisterAction();
    }
}
