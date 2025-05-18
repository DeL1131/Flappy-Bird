using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private CustomObjectPool<Bullet> _bulletPool;

    private void Awake()
    {
        _bulletPool = new CustomObjectPool<Bullet>(_bulletPrefab);
    }

    public Bullet SpawnBullet()
    {
        Bullet bullet = _bulletPool.Get();
        bullet.transform.position = transform.position;
        bullet.transform.right = transform.right;

        bullet.Returned += ReturnBullet;

        return bullet;
    }

    private void ReturnBullet(Bullet bullet)
    {
        bullet.Returned -= ReturnBullet;
        _bulletPool.ReturnToPool(bullet);
    }
}