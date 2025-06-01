using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += ShowInfo;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= ShowInfo;
    }

    private void ShowInfo(int score)
    {
        _text.text = score.ToString();
    }
}