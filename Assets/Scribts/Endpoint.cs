using UnityEngine;

public class Endpoint : MonoBehaviour
{
    private bool playerIsAtDoor = false;

    private void Update()
    {
        // Check for player input when they are at the door
        if (playerIsAtDoor && Input.GetKeyDown(KeyCode.W))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.WinGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the door's trigger zone
        if (other.CompareTag("Player"))
        {
            playerIsAtDoor = true;
            Debug.Log("Player is at the door. Press 'W' to win!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player has left the door's trigger zone
        if (other.CompareTag("Player"))
        {
            playerIsAtDoor = false;
        }
    }
}