using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Hurtbox>(out Hurtbox hurtbox))
        {
            hurtbox.TakeDamage(damage);
        }
    }
}