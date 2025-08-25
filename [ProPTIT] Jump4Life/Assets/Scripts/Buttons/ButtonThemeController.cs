using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonThemeController : MonoBehaviour
{
    private ThemeData _themeData;
    private Sprite _background;
    private Sprite[] _paddle;
    private Sprite _wall;
    private string _name;  
    [SerializeField] private Image _image;
    [SerializeField] private Image _tickImage; // Image cho dấu tick
    [SerializeField] private TextMeshProUGUI _themeNameText;
    private void Awake()
    {
        _image = GetComponent<Image>();
        
        // // Ẩn dấu tick ban đầu
        // if (_tickImage != null)
        // {
        //     _tickImage.gameObject.SetActive(false);
        // }
    }
    private void Start()
    {
        _image.sprite = _themeData.Background;
        _name = _themeData.ThemeName;
        _paddle = _themeData.Paddle;
        _wall = _themeData.Wall;
    }
    public void SetThemeData(ThemeData themeData)
    {
        _themeData = themeData;
    }
    public void SetThemeNameText(TextMeshProUGUI textMeshPro)
    {
        _themeNameText = textMeshPro;
    }
    public void OnClickThis()
    {
        _themeNameText.text = _name;
        GameManager.Instance.SetCurrentBackGround(_image.sprite);
        GameManager.Instance.SaveSelectedTheme(_name);
        //GameManager.Instance.SetCurrentThemeData(_themeData);
        PaddlesManager.Instance.SetPaddleImages(_paddle);
        WallManager.Instance.SetWallImage(_wall);
    }
}
