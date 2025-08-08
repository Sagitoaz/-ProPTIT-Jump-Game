using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Configuration")]
    [SerializeField] private float _timeScale = 1.0f;
    
    [Header("Game State")]
    [SerializeField] private int _score = 0;
    
    public int Score => _score;

    private void Awake()
    {
        InitializeSingleton();
        InitializeGameSettings();
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
        Time.timeScale = _timeScale; // Slow down time to debug
    }

    public void IncreaseScore(int value)
    {
        _score += value;
        OnScoreChanged();
    }

    private void OnScoreChanged()
    {
        // TODO: Update UI
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
}
