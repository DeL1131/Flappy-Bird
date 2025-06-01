using JetBrains.Annotations;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    [SerializeField] private BulletSpawner _playerBulletSpawner;
    [SerializeField] private BulletSpawner _enemyBulletSpawner;


    public BulletSpawner PlayerBulletSpawner => _playerBulletSpawner;
    public BulletSpawner EnemyBulletSpawner => _enemyBulletSpawner;

}