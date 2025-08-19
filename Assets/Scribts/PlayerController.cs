using UnityEngine;

/// <summary>
/// A simple script for 2D side-scrolling player movement.
/// Controls: 'A' and 'D' for horizontal movement, 'Space' for jumping.
/// Now includes firing with the Left Mouse Button.
/// </summary>
public class SimplePlayerMovement : MonoBehaviour
{
    // Existing variables for movement.
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;

    // A reference to the gun controller script.
    [SerializeField] private GunController gunController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Check for Left Mouse Button click to fire.
        // The isGrounded check has been removed.
        if (Input.GetMouseButtonDown(0))
        {
            if (gunController != null)
            {
                gunController.Fire();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}