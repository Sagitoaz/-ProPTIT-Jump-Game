using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Theme", menuName = "Theme Data", order = 1)]
public class ThemeData : ScriptableObject
{
    public string ThemeName;
    public Sprite Background;
    public Sprite[] Paddle;
    public Sprite Wall;
}
