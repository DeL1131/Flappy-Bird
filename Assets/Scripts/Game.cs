using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endScreen;
    [SerializeField] private EnemyGenerator _enemyGenerator;
    [SerializeField] private ScoreCounter _scoreCounter;

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

    private void DeactivateAllBullets()
    {
        Bullet[] bullets = FindObjectsOfType<Bullet>();

        foreach (Bullet bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }
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
        DeactivateAllBullets();
        _enemyGenerator.DeactivateAllObjects();
        _startScreen.Open();
        _endScreen.Open();
        Time.timeScale = 0f;
    }
}