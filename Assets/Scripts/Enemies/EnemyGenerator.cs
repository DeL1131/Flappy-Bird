using System;
using System.Collections;
using UnityEngine;

public abstract class EnemyGenerator : MonoBehaviour
{
    [SerializeField] protected BootStrapper BootStrapper;
    [SerializeField] protected Enemy Prefab;
    [SerializeField] protected float Delay;
    [SerializeField] protected float LowerBound;
    [SerializeField] protected float UpperBound;

    protected CustomObjectPool<Enemy> Pool;

    public event Action EnemyKilled;

    private void Start()
    {
        Pool = new CustomObjectPool<Enemy>(Prefab);
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

        Enemy enemy = Pool.Get();

        if(enemy.TryGetComponent<RangeAttacker>(out RangeAttacker rangeAttacker))
        {
            rangeAttacker.InjectDependencies(BootStrapper.EnemyBulletSpawner);
        }

        enemy.OnCollided += Return;
        enemy.OnDied += Return;
        enemy.OnDied += ReportEnemyKilled;
        enemy.transform.position = spawnPoint;
    }

    protected void Return(Enemy enemy)
    {
        enemy.OnCollided -= Return;
        enemy.OnDied -= Return;
        enemy.OnDied -= ReportEnemyKilled;
        Pool.ReturnToPool(enemy);
        enemy.Reset();
    }

    protected void ReportEnemyKilled(Enemy enemy)
    {
        EnemyKilled?.Invoke();
    }
}