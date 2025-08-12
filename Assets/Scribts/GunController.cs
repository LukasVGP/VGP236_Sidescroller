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

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Debug log to confirm if the AudioSource component is missing
            Debug.LogWarning("AudioSource component not found on the gun GameObject. No sound will be played.");
        }
    }

    /// <summary>
    /// This is the public method to fire the gun.
    /// It performs all the shooting actions.
    /// </summary>
    public void Fire()
    {
        // 1. Play the shooting sound effect.
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // 2. Spawn the muzzle flash effect at the projectile spawn point.
        if (muzzleFlashPrefab != null && projectileSpawnPoint != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation, projectileSpawnPoint);
            Destroy(flash, 0.1f);
        }

        // 3. Spawn the bullet and apply a forward force.
        if (bulletPrefab != null && projectileSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Verify the projectile force is greater than zero
                if (projectileForce <= 0)
                {
                    Debug.LogWarning("Projectile Force is 0 or less. Bullet will not move.");
                    return; // Exit the function if force is zero
                }

                // Apply a force in the direction the spawn point is facing.
                rb.AddForce(projectileSpawnPoint.right * projectileForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogError("Bullet prefab is missing a Rigidbody2D component!");
            }
        }

        // 4. Trigger the visual recoil animation on the barrel.
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
