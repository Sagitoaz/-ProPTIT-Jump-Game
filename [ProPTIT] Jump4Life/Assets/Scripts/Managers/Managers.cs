using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    [SerializeField] private PlayerController _player;
    public PlayerController GetPlayerController()
    {
        return _player;
    }
}
