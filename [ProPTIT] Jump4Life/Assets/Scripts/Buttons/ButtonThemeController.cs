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
    private bool _isSelected = false;
    
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
        GameManager.Instance.SetCurrentThemeData(_themeData);
        PaddlesManager.Instance.SetPaddleImages(_paddle);
        WallManager.Instance.SetWallImage(_wall);
        
        // // Thông báo cho ThemeSelectionManager rằng button này được chọn
        // if (ThemeSelectionManager.Instance != null)
        // {
        //     ThemeSelectionManager.Instance.SelectTheme(this);
        // }
    }
    
    /// <summary>
    /// Set trạng thái selected cho button này
    /// Được gọi bởi ThemeSelectionManager
    /// </summary>
    /// <param name="selected">True nếu button được chọn, false nếu không</param>
    // public void SetSelected(bool selected)
    // {
    //     _isSelected = selected;
        
    //     if (_tickImage != null)
    //     {
    //         _tickImage.gameObject.SetActive(selected);
    //     }
    // }
    
    // /// <summary>
    // /// Kiểm tra xem button có đang được chọn không
    // /// </summary>
    // /// <returns>True nếu button đang được chọn</returns>
    // public bool IsSelected()
    // {
    //     return _isSelected;
    // }
    
    // /// <summary>
    // /// Public method để set tick image từ bên ngoài
    // /// </summary>
    // public void SetTickImage(Image tickImage)
    // {
    //     _tickImage = tickImage;
    //     if (_tickImage != null)
    //     {
    //         _tickImage.gameObject.SetActive(_isSelected);
    //     }
    // }
    
    // /// <summary>
    // /// Lấy theme data của button này
    // /// </summary>
    // public ThemeData GetThemeData()
    // {
    //     return _themeData;
    // }
}
