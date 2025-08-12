using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;

    private float currentLifetime;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Spawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        rb.linearVelocity = transform.right * speed;
        currentLifetime = lifetime;
        gameObject.SetActive(true);
    }
}