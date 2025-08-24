using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Configuration")]
    [SerializeField] private float _timeScale = 1.0f;
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _currentCharacter;
    [SerializeField] private string _selectedCharacterName = "";
    private CharacterData _currentCharacterData;

    [Header("Game State")]
    [SerializeField] private int _score = 0;
    private int _highScore = 0;
    [SerializeField] public bool _isReplayGame = false;
    [SerializeField] private bool _isRestartGame = false;
    public override void Awake()
    {
        base.Awake();
        InitializeGameSettings();
        _highScore = PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
        _selectedCharacterName = PlayerPrefs.GetString(GameConfig.SELECTED_CHARACTER_KEY, GameConfig.DEFAULT_CHARACTER_NAME);

        if (string.IsNullOrEmpty(PlayerPrefs.GetString(GameConfig.SELECTED_CHARACTER_KEY, "")))
        {
            SaveSelectedCharacter(GameConfig.DEFAULT_CHARACTER_NAME);
        }
    }
    private void Start()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_MAINMENU);
        PanelManager.Instance.UpdateScore(_score);
        SetTimeScale(0);
        LoadSavedCharacter();
    }
    private void FixedUpdate()
    {
        if (_isReplayGame)
        {
            StartGame();
            _isReplayGame = !_isReplayGame;
            _isRestartGame = false;
            _player = Managers.Instance.GetPlayerController();
            LoadSavedCharacter();
        }
        if (_isRestartGame)
        {
            SetTimeScale(0);
            _isRestartGame = false;
            _player = Managers.Instance.GetPlayerController();
            LoadSavedCharacter();
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
        _isRestartGame = true;
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
    public void SaveSelectedCharacter(string characterName)
    {
        _selectedCharacterName = characterName;
        PlayerPrefs.SetString(GameConfig.SELECTED_CHARACTER_KEY, characterName);
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
    public string GetSelectedCharacterName()
    {
        return _selectedCharacterName;
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
}
