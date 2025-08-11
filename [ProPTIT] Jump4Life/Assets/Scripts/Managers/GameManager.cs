using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Configuration")]
    [SerializeField] private float _timeScale = 1.0f;

    [Header("Game State")]
    [SerializeField] private int _score = 0;
    private int _highScore = 0;
    private void Awake()
    {
        InitializeSingleton();
        InitializeGameSettings();
        _highScore = PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
    }
    private void Start()
    {
        UIManager.Instance.TurnMainMenu(true);
        SetTimeScale(0);
    }
    private void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void InitializeGameSettings()
    {
        Time.timeScale = _timeScale;
    }

    public void IncreaseScore(int value)
    {
        _score += value;
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
        SetTimeScale(_timeScale);
    }
    public void SetTimeScale(float timeScale)
    {
        _timeScale = timeScale;
        Time.timeScale = _timeScale;
    }
    public void StartGame()
    {
        UIManager.Instance.TurnMainMenu(false);
        SetTimeScale(1);
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
}
