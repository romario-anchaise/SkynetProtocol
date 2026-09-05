using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float acceleration = 45f;
    [SerializeField] private float deceleration = 55f;
    [SerializeField] private float jumpForce = 11f;
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.12f;

    [Header("Deteccion del suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.18f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private float horizontalInput;
    private bool isGrounded;
    private float coyoteTimer;
    private float jumpBufferTimer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        float left = keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed ? 1f : 0f;
        float right = keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed ? 1f : 0f;
        horizontalInput = right - left;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        coyoteTimer = isGrounded ? coyoteTime : coyoteTimer - Time.deltaTime;
        jumpBufferTimer = keyboard.spaceKey.wasPressedThisFrame
            ? jumpBufferTime
            : jumpBufferTimer - Time.deltaTime;

        if (keyboard.spaceKey.wasReleasedThisFrame && body.linearVelocity.y > 0f)
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * 0.5f);

        if (horizontalInput != 0f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(horizontalInput);
            transform.localScale = scale;
        }
    }

    private void FixedUpdate()
    {
        float targetSpeed = horizontalInput * moveSpeed;
        float changeRate = horizontalInput == 0f ? deceleration : acceleration;
        float smoothSpeed = Mathf.MoveTowards(body.linearVelocity.x, targetSpeed, changeRate * Time.fixedDeltaTime);
        body.linearVelocity = new Vector2(smoothSpeed, body.linearVelocity.y);

        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
