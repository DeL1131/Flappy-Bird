using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private EnemyGenerator _redBirdGenerator;
    [SerializeField] private EnemyGenerator _smallBirdGenerator;

    private readonly int _scoreForKillEnemy = 10;
    private readonly int _scoreForOneSecondPlay = 1;

    private bool _isCoroutineActive = false;
    private float _delayAddScore = 1f;

    public event Action<int> ScoreChanged;

    public int CurrentScore { get; private set; }

    private void OnEnable()
    {
        _isCoroutineActive = true;
        _redBirdGenerator.EnemyKilled += AddScoreForKill;
        _smallBirdGenerator.EnemyKilled += AddScoreForKill;
    }

    private void OnDisable()
    {
        _isCoroutineActive = false;
        _redBirdGenerator.EnemyKilled -= AddScoreForKill;
        _smallBirdGenerator.EnemyKilled -= AddScoreForKill;
    }

    public void Reset()
    {
        SetScore(0);
    }

    private void AddScoreForKill()
    {
        SetScore(_scoreForKillEnemy);
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