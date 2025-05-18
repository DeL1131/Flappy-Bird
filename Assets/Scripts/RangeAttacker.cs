using UnityEngine;

public class RangeAttacker : AttackType
{
    [SerializeField] private BulletSpawner _bulletSpawner;

    public override void ExecuteAttack()
    {
        Bullet bullet = _bulletSpawner.SpawnBullet();
        bullet.SetDamage(Damage);
        bullet.SetLayerMask(LayerMask);

        InvokeAttacked();
    }
}