using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelGameOver : PanelController
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private void OnEnable()
    {
        _scoreText.text = GameManager.Instance.GetScore().ToString();
    }
    public void OnClickHomeButton()
    {
        GameManager.Instance.RestartGame();
    }
    public void OnClickRestartButton()
    {
        GameManager.Instance.ReplayGame();
        GameManager.Instance.RestartGame();
    }
}
