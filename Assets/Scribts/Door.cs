using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool playerIsAtDoor = false;

    [Header("Door Settings")]
    [Tooltip("Check if this is the final door to end the game.")]
    [SerializeField] private bool isFinalDoor = false;
    [Tooltip("The name of the next level to load.")]
    [SerializeField] private string nextLevelName;

    private void Update()
    {
        // Check for player input when they are at the door
        if (playerIsAtDoor && Input.GetKeyDown(KeyCode.W))
        {
            if (GameManager.Instance != null)
            {
                if (isFinalDoor)
                {
                    GameManager.Instance.WinGame();
                }
                else
                {
                    GameManager.Instance.LoadNextLevel(nextLevelName);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the door's trigger zone
        if (other.CompareTag("Player"))
        {
            playerIsAtDoor = true;
            Debug.Log("Player is at the door. Press 'W' to proceed!");
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