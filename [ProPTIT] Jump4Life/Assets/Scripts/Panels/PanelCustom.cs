using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelCustom : PanelController
{
    [SerializeField] private ButtonController _avatarSelector;
    [SerializeField] private ButtonThemeController _themeSelector;
    [SerializeField] GameObject _changeAvatarBoard;
    [SerializeField] GameObject _changeThemeBoard;
    [SerializeField] private Transform _avatartBoard;
    [SerializeField] private Transform _themeBoard;
    [SerializeField] private Image _fullBodyArea;
    [SerializeField] private TextMeshProUGUI _characterName;
    [SerializeField] private TextMeshProUGUI _themeName;
    public void Start()
    {
        LoadAvatar();
        LoadTheme();
        _changeAvatarBoard.gameObject.SetActive(true);
        _changeThemeBoard.gameObject.SetActive(false);
        _fullBodyArea.preserveAspect = true;
    }
    public void OnEnable()
    {
        _changeAvatarBoard.gameObject.SetActive(true);
        _changeThemeBoard.gameObject.SetActive(false);
    }
    public void OnClickExitButton()
    {
        ClosePanel();
    }
    public void OnClickChangeAvatar()
    {
        _changeThemeBoard.gameObject.SetActive(false);
        _changeAvatarBoard.gameObject.SetActive(true);
    }
    public void OnClickChangeTheme()
    {
        _changeThemeBoard.gameObject.SetActive(true);
        _changeAvatarBoard.gameObject.SetActive(false);
    }
    private void LoadTheme()
    {
        ThemeData[] themes = Resources.LoadAll<ThemeData>(GameConfig.THEME_PATH);
        string selectedThemeName = GameManager.Instance.GetSelectedThemeName();

        foreach (ThemeData theme in themes)
        {
            ButtonThemeController themeButton = Instantiate(_themeSelector, _themeBoard);
            themeButton.SetThemeData(theme);
            themeButton.SetThemeNameText(_themeName);

            if (theme.ThemeName == selectedThemeName)
            {
                _themeName.text = theme.ThemeName;
            }
        }
    }
    private void LoadAvatar()
    {
        CharacterData[] avatars = Resources.LoadAll<CharacterData>(GameConfig.CHARACTER_PATH);
        string selectedCharacterName = GameManager.Instance.GetSelectedCharacterName();

        if (string.IsNullOrEmpty(selectedCharacterName))
        {
            selectedCharacterName = GameConfig.DEFAULT_CHARACTER_NAME;
        }

        foreach (CharacterData avatar in avatars)
        {
            ButtonController avatarButton = Instantiate(_avatarSelector, _avatartBoard);
            avatarButton.SetCharacterData(avatar);
            avatarButton.SetFullBodyArea(_fullBodyArea);
            avatarButton.SetCharacterName(_characterName);

            if (avatar.CharacterName == selectedCharacterName)
            {
                _fullBodyArea.sprite = avatar.FullBody;
                _characterName.text = avatar.CharacterName;
            }
        }
    }
}
