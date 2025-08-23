using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Sprite _fullbody;
    private Image _image;
    private Image _fullBodyArea;
    private string _name;
    private TextMeshProUGUI _characterName;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    public void SetIcon(Sprite sprite)
    {
        _image.sprite = sprite;
    }
    public void SetFullBody(Sprite sprite)
    {
        _fullbody = sprite;
    }
    public void SetFullBodyArea(Image image)
    {
        _fullBodyArea = image;
    }
    public void SetCharacterName(TextMeshProUGUI textMeshPro)
    {
        _characterName = textMeshPro;
    }
    public void SetName(string name)
    {
        _name = name;
    }
    public void OnClickThis()
    {
        _fullBodyArea.sprite = _fullbody;
        _characterName.text = _name;
    }
}
