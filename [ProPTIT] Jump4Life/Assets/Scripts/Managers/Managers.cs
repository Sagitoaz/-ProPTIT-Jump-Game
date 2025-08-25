using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Image _background;
    public PlayerController GetPlayerController()
    {
        return _player;
    }
    public Image GetBackgroundImage()
    {
        return _background;
    }
}
