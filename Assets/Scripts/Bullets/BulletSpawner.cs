using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private CustomObjectPool<Bullet> _bulletPool;

    private void Awake()
    {
        _bulletPool = new CustomObjectPool<Bullet>(_bulletPrefab);
    }

    public Bullet SpawnBullet(Transform spawnTransform)
    {
        Bullet bullet = _bulletPool.Get();
        bullet.transform.position = spawnTransform.position;
        bullet.transform.right = spawnTransform.right;

        bullet.Returned += ReturnBullet;

        return bullet;
    }

    public void DeactivateAllObjects()
    {
        _bulletPool.DeactivateAll();
    }

    private void ReturnBullet(Bullet bullet)
    {
        bullet.Returned -= ReturnBullet;
        _bulletPool.ReturnToPool(bullet);
    }
}