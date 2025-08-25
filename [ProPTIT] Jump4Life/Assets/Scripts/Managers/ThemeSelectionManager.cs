// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ThemeSelectionManager : MonoBehaviour
// {
//     public static ThemeSelectionManager Instance { get; private set; }
    
//     private ButtonThemeController _currentSelectedButton;
    
//     private void Awake()
//     {
//         // Singleton pattern
//         if (Instance == null)
//         {
//             Instance = this;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
    
//     /// <summary>
//     /// Được gọi khi một button theme được click
//     /// </summary>
//     /// <param name="clickedButton">Button được click</param>
//     public void SelectTheme(ButtonThemeController clickedButton)
//     {
//         // Nếu có button đang được chọn trước đó, hủy chọn nó
//         if (_currentSelectedButton != null && _currentSelectedButton != clickedButton)
//         {
//             _currentSelectedButton.SetSelected(false);
//         }
        
//         // Set button mới được chọn
//         _currentSelectedButton = clickedButton;
//         if (_currentSelectedButton != null)
//         {
//             _currentSelectedButton.SetSelected(true);
//         }
//     }
    
//     /// <summary>
//     /// Lấy button theme hiện tại đang được chọn
//     /// </summary>
//     /// <returns>Button theme đang được chọn, null nếu không có</returns>
//     public ButtonThemeController GetSelectedButton()
//     {
//         return _currentSelectedButton;
//     }
    
//     /// <summary>
//     /// Hủy chọn button hiện tại
//     /// </summary>
//     public void DeselectCurrent()
//     {
//         if (_currentSelectedButton != null)
//         {
//             _currentSelectedButton.SetSelected(false);
//             _currentSelectedButton = null;
//         }
//     }
// }
