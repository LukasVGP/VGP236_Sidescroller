using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singleton pattern
    public static GameManager Instance { get; private set; }

    // Player Stats
    [Header("Player Stats")]
    [SerializeField] private float playerMaxHealth = 100f;
    private float playerCurrentHealth;
    [SerializeField] private int playerMaxAmmo = 50;
    private int playerCurrentAmmo;

    // Game State
    [Header("Game State")]
    [SerializeField] private int currentPoints = 0;
    [SerializeField] private UIController uiController;

    private void Awake()
    {
        // Enforce a single instance of the GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        playerCurrentHealth = playerMaxHealth;
        playerCurrentAmmo = playerMaxAmmo;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (uiController != null)
        {
            uiController.UpdateHealthBar(playerCurrentHealth / playerMaxHealth);
            uiController.UpdateAmmoText(playerCurrentAmmo);
            uiController.UpdatePointsText(currentPoints);
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;
        UpdateUI();
    }

    public void AddAmmo(int ammoToAdd)
    {
        playerCurrentAmmo += ammoToAdd;
        if (playerCurrentAmmo > playerMaxAmmo)
        {
            playerCurrentAmmo = playerMaxAmmo;
        }
        UpdateUI();
    }

    public void PlayerTakeDamage(float damage)
    {
        playerCurrentHealth -= damage;
        if (playerCurrentHealth < 0)
        {
            playerCurrentHealth = 0;
        }
        Debug.Log("Player Health: " + playerCurrentHealth);
        UpdateUI();
        if (playerCurrentHealth <= 0)
        {
            // Trigger Lose Condition
            LoseGame();
        }
    }

    public void PlayerHeal(float healAmount)
    {
        playerCurrentHealth += healAmount;
        if (playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
        UpdateUI();
    }

    public void UseAmmo(int amount = 1)
    {
        playerCurrentAmmo -= amount;
        if (playerCurrentAmmo < 0)
        {
            playerCurrentAmmo = 0;
        }
        UpdateUI();
    }

    public int GetCurrentAmmo()
    {
        return playerCurrentAmmo;
    }

    public void NotifyEnemyKilled(int points)
    {
        AddPoints(points);
    }

    // Level Transition
    public void LoadNextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // Win/Lose Conditions
    public void WinGame()
    {
        Debug.Log("You Win!");
        // You would typically load a new scene or show a UI panel here.
    }

    public void LoseGame()
    {
        Debug.Log("Game Over!");
        // You would typically load a new scene or show a UI panel here.
    }
}