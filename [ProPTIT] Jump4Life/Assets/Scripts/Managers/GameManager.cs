using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Configuration")]
    [SerializeField] private float _timeScale = 1.0f;

    [Header("Game State")]
    [SerializeField] private int _score = 0;
    private int _highScore = 0;
    [SerializeField] public bool _isReplayGame = false;
    public override void Awake()
    {
        base.Awake();
        InitializeGameSettings();
        _highScore = PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
    }
    private void Start()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_MAINMENU);
        PanelManager.Instance.UpdateScore(_score);
        SetTimeScale(0);
    }
    private void FixedUpdate()
    {
        if (_isReplayGame)
        {
            _isReplayGame = !_isReplayGame;
            StartGame();
        }
    }
    private void InitializeGameSettings()
    {
        Time.timeScale = _timeScale;
    }

    public void IncreaseScore(int value)
    {
        _score += value;
        PanelManager.Instance.UpdateScore(_score);
        if (_score > _highScore)
        {
            UpdateHighScore();
        }
    }
    public void PauseGame()
    {
        SetTimeScale(0f);
    }
    public void ResumeGame()
    {
        SetTimeScale(1);
    }
    public void SetTimeScale(float timeScale)
    {
        _timeScale = timeScale;
        Time.timeScale = _timeScale;
    }
    public void StartGame()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_INGAME);
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_PAUSE_MENU);
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_MAINMENU);
        SetTimeScale(1);
    }
    public void RestartGame()
    {
        _score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReplayGame()
    {
        _isReplayGame = true;
    }
    private void UpdateHighScore()
    {
        _highScore = _score;
        PlayerPrefs.SetInt(GameConfig.HIGH_SCORE_KEY, _highScore);
    }
    public int GetHighScore()
    {
        return _highScore;
    }
    public int GetScore()
    {
        return _score;
    }
}
