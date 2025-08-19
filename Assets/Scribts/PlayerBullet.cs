using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    // Add OnTriggerEnter2D to handle hitting enemies with the PlayerBullet's Hitbox.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Call the TakeDamage method on the enemy and destroy the bullet.
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(35f);
                enemy.ApplyKnockback(transform.position);
                Destroy(gameObject);
            }
        }
    }
}