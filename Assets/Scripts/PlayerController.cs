using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 11f;

    [Header("Deteccion del suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.18f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private float horizontalInput;
    private bool jumpRequested;
    private bool isGrounded;

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

        if (keyboard.spaceKey.wasPressedThisFrame && isGrounded)
            jumpRequested = true;

        if (horizontalInput != 0f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(horizontalInput);
            transform.localScale = scale;
        }
    }

    private void FixedUpdate()
    {
        body.linearVelocity = new Vector2(horizontalInput * moveSpeed, body.linearVelocity.y);

        if (jumpRequested)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            jumpRequested = false;
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
