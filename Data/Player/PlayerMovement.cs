using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D BodyPlayer;
    public float Speed = 200f;

    private bool canMove = false;
    public bool movementLocked = false;

    private float horizontal;
    private float vertical;
    private Vector2 movement;

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    void Update()
    {
        if (!canMove || movementLocked) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            TimeManager.Instance?.RegisterAction();
        }
    }

    void FixedUpdate()
    {
        if (!canMove || movementLocked) return;

        movement = new Vector2(horizontal, vertical).normalized;
        BodyPlayer.linearVelocity = movement * Speed;
    }
}
