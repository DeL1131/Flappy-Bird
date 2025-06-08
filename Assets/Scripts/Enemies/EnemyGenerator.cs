using System;
using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] protected BulletSpawner _enemyBulletSpawner;
    [SerializeField] protected BirdEnemy Prefab;
    [SerializeField] protected float Delay;
    [SerializeField] protected float LowerBound;
    [SerializeField] protected float UpperBound;

    protected CustomObjectPool<BirdEnemy> Pool;

    public event Action EnemyKilled;

    private void Start()
    {
        Pool = new CustomObjectPool<BirdEnemy>(Prefab);
        StartCoroutine(GenerateEnemy());
    }

    public void DeactivateAllObjects()
    {
        Pool.DeactivateAll();
    }

    protected IEnumerator GenerateEnemy()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(Delay);

        while (enabled)
        {
            Spawn();
            yield return waitForSeconds;
        }
    }

    protected void Spawn()
    {
        float spawnPositionY = UnityEngine.Random.Range(UpperBound, LowerBound);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        BirdEnemy enemy = Pool.Get();

        if(enemy.TryGetComponent<RangeAttacker>(out RangeAttacker rangeAttacker))
        {
            rangeAttacker.InjectDependencies(_enemyBulletSpawner);
        }

        enemy.OnCollided += Return;
        enemy.OnDied += Return;
        enemy.OnDied += ReportEnemyKilled;
        enemy.transform.position = spawnPoint;
    }

    protected void Return(BirdEnemy enemy)
    {
        enemy.OnCollided -= Return;
        enemy.OnDied -= Return;
        enemy.OnDied -= ReportEnemyKilled;
        Pool.ReturnToPool(enemy);
        enemy.Reset();
    }

    protected void ReportEnemyKilled(BirdEnemy enemy)
    {
        EnemyKilled?.Invoke();
    }
}