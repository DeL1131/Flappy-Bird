using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private LayerMask _layerMask;

    private CustomObjectPool<Bullet> _bulletPool;

    private void Awake()
    {
        _bulletPool = new CustomObjectPool<Bullet>(_bulletPrefab);
    }

    public void Attack(float damage)
    {
        SpawnBullet(damage);
    }

    private void SpawnBullet( float damage)
    {
        Bullet bullet = _bulletPool.Get();
        bullet.SetDamage(damage);
        bullet.SetLayerMask(_layerMask);
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.transform.right = _bulletSpawnPoint.transform.right;

        bullet.Returned += ReturnBullet;
    }

    private void ReturnBullet(Bullet bullet)
    {
        bullet.Returned -= ReturnBullet;
        _bulletPool.ReturnToPool(bullet);
    }
}