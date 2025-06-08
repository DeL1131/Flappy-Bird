using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endScreen;
    [SerializeField] private ScoreCounter _scoreCounter;

    [SerializeField] private EnemyGenerator _redBirdGenerator;
    [SerializeField] private EnemyGenerator _smallBirdGenerator;
    [SerializeField] private BulletSpawner _enemyBulletSpawner;
    [SerializeField] private BulletSpawner _playerBulletSpawner;

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _bird.GameOver += EndGame;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _bird.GameOver -= EndGame;
    }

    private void DeactivateTemporaryObjects()
    {
        _enemyBulletSpawner.DeactivateAllObjects();
        _playerBulletSpawner.DeactivateAllObjects();
        _redBirdGenerator.DeactivateAllObjects();
        _smallBirdGenerator.DeactivateAllObjects();
    }

    private void OnPlayButtonClick()
    {
        _endScreen.Close();
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1.0f;
        _bird.Reset();
        _scoreCounter.Reset();
    }

    private void EndGame()
    {
        DeactivateTemporaryObjects();
        _startScreen.Open();
        _endScreen.Open();
        Time.timeScale = 0f;
    }
}