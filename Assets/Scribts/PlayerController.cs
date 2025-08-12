using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private float moveSpeed = 5f;
    private float jumpSpeed = 10f;
    private int maxJumps = 1;
    private int numJumps;

    private Vector2 moveInput;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // This is where you would enable input actions if not using PlayerInput component
    }

    private void OnDisable()
    {
        // This is where you would disable input actions if not using PlayerInput component
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (numJumps < maxJumps)
        {
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, jumpSpeed);
            numJumps++;
        }
    }

    private void FixedUpdate()
    {
        playerRB.linearVelocity = new Vector2(moveInput.x * moveSpeed, playerRB.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        numJumps = 0;
    }
}