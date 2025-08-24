using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private CharacterData _characterData;
    private Sprite _fullbody;
    private Image _image;
    private Image _fullBodyArea;
    private string _name;
    private GameObject _character;
    private TextMeshProUGUI _characterName;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    private void Start()
    {
        _image.sprite = _characterData.Icon;
        _fullbody = _characterData.FullBody;
        _name = _characterData.CharacterName;
        _character = _characterData.Character;
    }
    public void SetFullBodyArea(Image image)
    {
        _fullBodyArea = image;
    }
    public void SetCharacterName(TextMeshProUGUI textMeshPro)
    {
        _characterName = textMeshPro;
    }
    public void SetCharacterData(CharacterData characterData)
    {
        _characterData = characterData;
    }
    public void OnClickThis()
    {
        _fullBodyArea.sprite = _fullbody;
        _characterName.text = _name;
        GameObject newCharacter = Instantiate(_character, GameManager.Instance.GetPlayerTransform());
        GameManager.Instance.SetCurrentCharacter(newCharacter);
        GameManager.Instance.SetCurrentCharacterData(_characterData);
        GameManager.Instance.SaveSelectedCharacter(_name);
    }
}
