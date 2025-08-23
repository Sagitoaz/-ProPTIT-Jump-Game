using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelCustom : PanelController
{
    [SerializeField] private ButtonController _avatarSelector;
    [SerializeField] private Transform _avatartBoard;
    [SerializeField] private Image _fullBodyArea;
    [SerializeField] private TextMeshProUGUI _characterName;
    public void Start()
    {
        LoadAvatar();
        _fullBodyArea.preserveAspect = true;
    }
    public void OnClickExitButton()
    {
        ClosePanel();
    }
    public void LoadAvatar()
    {
        CharacterData[] avatars = Resources.LoadAll<CharacterData>(GameConfig.CHARACTER_PATH);
        foreach (CharacterData avatar in avatars)
        {
            ButtonController avatarButton = Instantiate(_avatarSelector, _avatartBoard);
            avatarButton.SetIcon(avatar.Icon);
            avatarButton.SetFullBody(avatar.FullBody);
            avatarButton.SetFullBodyArea(_fullBodyArea);
            avatarButton.SetCharacterName(_characterName);
            avatarButton.SetName(avatar.CharacterName);
        }
    }
}
