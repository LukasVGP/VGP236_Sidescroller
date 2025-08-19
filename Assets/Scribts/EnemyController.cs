using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState { Wander, Pursue, Attack, ReturnToWander }

    [Header("General")]
    [SerializeField] private EnemyState currentState;
    [SerializeField] private int enemyPoints = 25;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private float knockbackForce = 5f;
    private float stunTime = 0f;

    [Header("Wander")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float moveRange = 5f;
    private int moveDirection = 1;
    private Vector3 startingPoint;

    [Header("Pursue")]
    private Transform pursueTarget;

    [Header("Attack")]
    [SerializeField] private bool isRangedEnemy = false;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;
    private float nextAttackTime;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPoint = transform.position;
        currentHealth = maxHealth;
        currentState = EnemyState.Wander;
    }

    private void Update()
    {
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
            return;
        }

        switch (currentState)
        {
            case EnemyState.Wander:
                DoWander();
                break;
            case EnemyState.Pursue:
                DoPursue();
                break;
            case EnemyState.Attack:
                DoAttack();
                break;
            case EnemyState.ReturnToWander:
                DoReturnToWander();
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.NotifyEnemyKilled(enemyPoints);
        Destroy(gameObject);
    }

    // Applies knockback force on hit
    public void ApplyKnockback(Vector2 hitPosition)
    {
        Vector2 knockbackDirection = ((Vector2)transform.position - hitPosition).normalized;
        rb.linearVelocity = Vector2.zero; // Stop current movement
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        stunTime = 0.5f; // Short stun
    }

    private void DoWander()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        if (Vector3.SqrMagnitude(transform.position - startingPoint) > moveRange * moveRange)
        {
            moveDirection *= -1;
        }
    }

    private void DoPursue()
    {
        if (pursueTarget == null)
        {
            currentState = EnemyState.Wander;
            return;
        }

        Vector2 direction = (pursueTarget.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        float distanceToTarget = Vector2.Distance(transform.position, pursueTarget.position);

        if (distanceToTarget < attackRange)
        {
            currentState = EnemyState.Attack;
        }

        if (distanceToTarget > moveRange * 1.5f) // Return to wander if too far
        {
            currentState = EnemyState.ReturnToWander;
        }
    }

    private void DoAttack()
    {
        if (pursueTarget == null)
        {
            currentState = EnemyState.Wander;
            return;
        }

        rb.linearVelocity = Vector2.zero; // Stop movement to attack

        if (isRangedEnemy)
        {
            if (Time.time > nextAttackTime)
            {
                Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else // Melee
        {
            // Melee enemies cause damage on contact (handled by OnTriggerEnter2D on Player)
        }

        float distanceToTarget = Vector2.Distance(transform.position, pursueTarget.position);
        if (distanceToTarget > attackRange)
        {
            currentState = EnemyState.Pursue;
        }
    }

    private void DoReturnToWander()
    {
        Vector2 direction = (startingPoint - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        if (Vector3.SqrMagnitude(transform.position - startingPoint) < 1f)
        {
            currentState = EnemyState.Wander;
        }
    }

    // Visual sensor detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentState != EnemyState.Attack)
        {
            pursueTarget = other.transform;
            currentState = EnemyState.Pursue;
        }
    }

    // Player Melee detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // This handles damage from the player's melee attack
        if (collision.gameObject.CompareTag("PlayerMelee"))
        {
            // You will need to add a Hitbox on the melee weapon
        }
    }
}