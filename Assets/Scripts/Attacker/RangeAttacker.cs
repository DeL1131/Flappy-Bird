using UnityEngine;

public class RangeAttacker : AttackComponent
{
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private BulletSpawner _bulletSpawner;


    public void InjectDependencies(BulletSpawner bulletSpawner)
    {
        _bulletSpawner = bulletSpawner;
    }

    public override void ExecuteAttack()
    {
        Bullet bullet = _bulletSpawner.SpawnBullet(_bulletSpawnTransform);
        bullet.SetDamage(Damage);
        bullet.SetLayerMask(LayerMask);

        InvokeAttacked();
    }
}