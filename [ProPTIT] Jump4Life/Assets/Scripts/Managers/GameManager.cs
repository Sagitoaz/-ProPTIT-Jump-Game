using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Configuration")]
    [SerializeField] private float _timeScale = 1.0f;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Image _background;
    [SerializeField] private GameObject _currentCharacter;
    [SerializeField] private string _selectedCharacterName = "";
    [SerializeField] private string _selectedThemeName = "";
    private CharacterData _currentCharacterData;
    [Header("Game State")]
    [SerializeField] private int _score = 0;
    private int _highScore = 0;
    [SerializeField] public bool _isReplayGame = false;
    [SerializeField] private bool _isRestartGame = false;
    [SerializeField] private int milestone = 10;
    public override void Awake()
    {
        base.Awake();
        InitializeGameSettings();
        _highScore = PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
        _selectedCharacterName = PlayerPrefs.GetString(GameConfig.SELECTED_CHARACTER_KEY, GameConfig.DEFAULT_CHARACTER_NAME);
        _selectedThemeName = PlayerPrefs.GetString(GameConfig.SELECTED_THEME_KEY, GameConfig.DEFAULT_THEME_NAME);

        if (string.IsNullOrEmpty(PlayerPrefs.GetString(GameConfig.SELECTED_CHARACTER_KEY, "")))
        {
            SaveSelectedCharacter(GameConfig.DEFAULT_CHARACTER_NAME);
        }
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(GameConfig.SELECTED_THEME_KEY, "")))
        {
            SaveSelectedTheme(GameConfig.DEFAULT_THEME_NAME);
        }
    }
    private void Start()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_MAINMENU);
        PanelManager.Instance.UpdateScore(_score);
        SetTimeScale(0);
        LoadSavedCharacter();
        LoadSavedTheme();
    }
    private void FixedUpdate()
    {
        if (_isReplayGame || _isRestartGame)
        {
            _isRestartGame = false;
            _player = Managers.Instance.GetPlayerController();
            _background = Managers.Instance.GetBackgroundImage();
            LoadSavedCharacter();
            LoadSavedTheme();
        }
        if (_isReplayGame)
        {
            StartGame();
            _isReplayGame = !_isReplayGame;
            
        }
        if (_isRestartGame)
        {
            SetTimeScale(0);
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
        if (_score >= milestone)
        {
            milestone += 10;
            PaddlePool.Instance.IncreaseSpeeds(1.2f);
        }
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
        _isRestartGame = true;
        SetTimeScale(0);
    }
    public void GameOver()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_GAMEOVER);
        SetTimeScale(0);
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
    public void SetCurrentCharacter(GameObject character)
    {
        if (_currentCharacter != null)
        {
            Destroy(_currentCharacter);
        }
        _currentCharacter = character;
        _player.SetAnimator(_currentCharacter.GetComponent<Animator>());
    }
    public void SetCurrentBackGround(Sprite background)
    {
        _background.sprite = background;
    }
    public void SaveSelectedCharacter(string characterName)
    {
        _selectedCharacterName = characterName;
        PlayerPrefs.SetString(GameConfig.SELECTED_CHARACTER_KEY, characterName);
        PlayerPrefs.Save();
    }
    public void SaveSelectedTheme(string themeName)
    {
        _selectedThemeName = themeName;
        PlayerPrefs.SetString(GameConfig.SELECTED_THEME_KEY, themeName);
        PlayerPrefs.Save();
    }
    public void LoadSavedCharacter()
    {
        string characterToSpawn = !string.IsNullOrEmpty(_selectedCharacterName) ? _selectedCharacterName : GameConfig.DEFAULT_CHARACTER_NAME;
        SpawnCharacterByName(characterToSpawn);
    }
    private void SpawnCharacterByName(string characterName)
    {
        CharacterData[] avatars = Resources.LoadAll<CharacterData>(GameConfig.CHARACTER_PATH);
        foreach (CharacterData avatar in avatars)
        {
            if (avatar.CharacterName == characterName)
            {
                GameObject newCharacter = Instantiate(avatar.Character, GetPlayerTransform());
                SetCurrentCharacter(newCharacter);
                SetCurrentCharacterData(avatar);
                break;
            }
        }
    }
    public void LoadSavedTheme()
    {
        string themeToLoad = !string.IsNullOrEmpty(_selectedThemeName) ? _selectedThemeName : GameConfig.DEFAULT_THEME_NAME;
        SetCurrentThemeByName(themeToLoad);
    }
    private void SetCurrentThemeByName(string themeName)
    {
        ThemeData[] themes = Resources.LoadAll<ThemeData>(GameConfig.THEME_PATH);
        foreach (ThemeData theme in themes)
        {
            if (theme.ThemeName == themeName)
            {
                SetCurrentBackGround(theme.Background);
                // SetCurrentThemeData(theme);
                PaddlesManager.Instance.SetPaddleImages(theme.Paddle);
                WallManager.Instance.SetWallImage(theme.Wall);
                break;
            }
        }
    }
    public string GetSelectedCharacterName()
    {
        return _selectedCharacterName;
    }
    public string GetSelectedThemeName()
    {
        return PlayerPrefs.GetString(GameConfig.SELECTED_THEME_KEY, GameConfig.DEFAULT_THEME_NAME);
    }
    public Transform GetPlayerTransform()
    {
        return _player.gameObject.transform;
    }
    public int GetHighScore()
    {
        return _highScore;
    }
    public int GetScore()
    {
        return _score;
    }
    public bool GetIsReplay()
    {
        return _isReplayGame;
    }
    public Sprite GetCurrentCharacterSprite()
    {
        return _currentCharacterData?.Icon;
    }
    public void SetCurrentCharacterData(CharacterData characterData)
    {
        _currentCharacterData = characterData;
    }
    // public void SetCurrentThemeData(ThemeData themeData)
    // {
    //     _currentThemeData = themeData;
    // }
}
