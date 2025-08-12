using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int maxBullets = 10;

    private List<Bullet> bulletPool;
    private int nextBulletIndex;

    private void Awake()
    {
        bulletPool = new List<Bullet>();
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject newObject = Instantiate(bulletPrefab);
            Bullet newBullet = newObject.GetComponent<Bullet>();
            bulletPool.Add(newBullet);
            newObject.SetActive(false);
        }
    }

    public void Fire()
    {
        Bullet bullet = bulletPool[nextBulletIndex];
        bullet.Spawn(bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        nextBulletIndex = (nextBulletIndex + 1) % bulletPool.Count;
    }
}