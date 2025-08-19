using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float damage = 25f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PlayerTakeDamage(damage);
            Destroy(gameObject);
        }
    }
}