using System;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : Window
{
    [SerializeField] private Button _playButton;

    public event Action PlayButtonClicked;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnButtonClick);
    }

    protected override void OnButtonClick()
    {
        PlayButtonClicked?.Invoke();
    }
}