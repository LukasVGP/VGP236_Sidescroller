using UnityEngine;

/// <summary>
/// This script is attached to the bullet prefab. It ensures the bullet
/// has a Rigidbody2D and destroys itself after a set time.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))] // Automatically adds a Rigidbody2D if not present
public class Bullet : MonoBehaviour
{
    // The amount of time in seconds before the bullet is destroyed.
    [SerializeField] private float lifetime = 2f;

    private void Start()
    {
        // Destroy the bullet's GameObject after the specified lifetime.
        Destroy(gameObject, lifetime);
    }
}
