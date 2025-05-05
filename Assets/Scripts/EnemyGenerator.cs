using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _delay;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;

    private List<Enemy> _activeObjects = new List<Enemy>();

    private CustomObjectPool<Enemy> _pool;

    private void Start()
    {
        _pool = new CustomObjectPool<Enemy>(_prefab);
        StartCoroutine(GenerateEnemy());
    }

    public void DeactivateAllObjects()
    {
        foreach (Enemy enemy in _activeObjects)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    private IEnumerator GenerateEnemy()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();
            yield return waitForSeconds;
        }
    }

    private void Spawn()
    {
        float spawnPositionY = Random.Range(_upperBound, _lowerBound);
        Vector3 SpawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        Enemy enemy = _pool.Get();
        _activeObjects.Add(enemy);
        enemy.OnCollided += Return;
        enemy.OnDied += Return;
        enemy.transform.position = SpawnPoint;
    }

    private void Return(Enemy enemy)
    {
        enemy.OnCollided -= Return;
        enemy.OnDied -= Return;
        _activeObjects.Remove(enemy);
        _pool.ReturnToPool(enemy);
        enemy.Reset();
    }
}