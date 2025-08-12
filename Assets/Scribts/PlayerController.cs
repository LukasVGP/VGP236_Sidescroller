using UnityEngine;

/// <summary>
/// A simple script for 2D side-scrolling player movement.
/// Controls: 'A' and 'D' for horizontal movement, 'Space' for jumping.
/// </summary>
public class SimplePlayerMovement : MonoBehaviour
{
    // Public variables to control player speed and jump force.
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    // A reference to the Rigidbody2D component.
    private Rigidbody2D rb;

    // A private bool to track if the player is currently touching the ground.
    private bool isGrounded;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Get the Rigidbody2D component attached to this GameObject.
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called once per frame to handle input.
    /// </summary>
    private void Update()
    {
        // Check for 'Space' key press, but only if the player is on the ground.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    /// <summary>
    /// FixedUpdate is called at a fixed interval for physics calculations.
    /// </summary>
    private void FixedUpdate()
    {
        // Get the value of the horizontal input axis.
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply velocity to the Rigidbody for horizontal movement.
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    /// <summary>
    /// Applies an upward force to the player to make them jump.
    /// </summary>
    private void Jump()
    {
        // Apply an upward velocity directly to the Rigidbody.
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    /// <summary>
    /// Called when the ground-checking collider enters a trigger.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider entering the trigger is on the "Ground" layer.
        // We use LayerMask.NameToLayer("Ground") to get the layer number.
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    /// <summary>
    /// Called when the ground-checking collider exits a trigger.
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider leaving the trigger is on the "Ground" layer.
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
