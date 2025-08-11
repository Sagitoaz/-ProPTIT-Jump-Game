using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        ShowHighScoreText(GameManager.Instance.GetHighScore());
    }
    public void TurnMainMenu(bool isActive)
    {
        _mainMenu.SetActive(isActive);
    }
    private void ShowHighScoreText(int highScore)
    {
        _highScoreText.text = highScore.ToString();
    }
}
