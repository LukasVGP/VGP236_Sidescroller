using UnityEngine;
using DG.Tweening;

/// <summary>
/// A single, combined script to control the blunderbuss.
/// Handles firing bullets, muzzle flash, audio, and barrel recoil animation.
/// </summary>
public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private float projectileForce = 20f;
    [SerializeField] private float recoilDistance = 0.2f;
    [SerializeField] private float recoilDuration = 0.05f;

    // Cooldown variables
    [SerializeField] private float fireRate = 0.3f;
    private float nextFireTime;

    private AudioSource audioSource;

    private void Awake()
    {
        // Get or add an AudioSource component if one is not present.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    /// <summary>
    /// This is the public method to fire the gun.
    /// It performs all the shooting actions, respecting the cooldown.
    /// </summary>
    public void Fire()
    {
        // Check if the gun is on cooldown. If Time.time is less than nextFireTime, return.
        if (Time.time < nextFireTime)
        {
            return;
        }

        // 1. Set the time for the next shot.
        nextFireTime = Time.time + fireRate;

        // 2. Play the shooting sound effect.
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // 3. Spawn the muzzle flash effect.
        if (muzzleFlashPrefab != null && projectileSpawnPoint != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation, projectileSpawnPoint);
            Destroy(flash, 0.1f);
        }

        // 4. Spawn the bullet and set its velocity.
        if (bulletPrefab != null && projectileSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Directly set the bullet's velocity.
                rb.linearVelocity = projectileSpawnPoint.right * projectileForce;
            }
            else
            {
                Debug.LogError("Bullet prefab is missing a Rigidbody2D component!");
            }
        }

        // 5. Trigger the visual recoil animation on the barrel.
        Recoil();
    }

    /// <summary>
    /// A private method to handle the barrel's recoil animation using DoTween.
    /// </summary>
    private void Recoil()
    {
        Vector3 originalLocalPosition = barrelTransform.localPosition;

        barrelTransform.DOLocalMoveX(originalLocalPosition.x - recoilDistance, recoilDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => {
                barrelTransform.DOLocalMoveX(originalLocalPosition.x, recoilDuration)
                    .SetEase(Ease.OutSine);
            });
    }
}