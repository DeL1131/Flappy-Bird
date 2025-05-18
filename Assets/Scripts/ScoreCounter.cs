using System;
using System.Collections;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private readonly int _scoreForKillEnemy = 10;
    private readonly int _scoreForOneSecondPlay = 1;

    [SerializeField] private EnemyGenerator _enemyGenerator;

    private bool _isCoroutineActive = false;
    private float _delayAddScore = 1f;

    public event Action<int> ScoreChanged;

    public int CurrentScore { get; private set; }

    private void OnEnable()
    {
        _isCoroutineActive = true;
        StartCoroutine(AddScoreForOneSecond());
        _enemyGenerator.EnemyKilled += AddScoreForKill;
    }

    private void OnDisable()
    {
        _isCoroutineActive = false;
        _enemyGenerator.EnemyKilled -= AddScoreForKill;
    }

    public void Reset()
    {
        SetScore(0);
    }

    private void AddScoreForKill()
    {
        SetScore(_scoreForKillEnemy);
    }

    private IEnumerator AddScoreForOneSecond()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delayAddScore);

        while (true)
        {
            yield return waitForSeconds;
            SetScore(_scoreForOneSecondPlay);
        }
    }

    private void SetScore(int amount)
    {
        if (amount != 0)
        {
            CurrentScore += amount;
        }
        else
        {
            CurrentScore = amount;
        }

        ScoreChanged?.Invoke(CurrentScore);
    }
}