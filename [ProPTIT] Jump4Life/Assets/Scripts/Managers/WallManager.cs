using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : Singleton<WallManager>
{
    [SerializeField] private WallController _leftWall, _rightWall;
    private Camera _camera;
    public float _halfWallWidth;
    public override void Awake()
    {
        base.Awake();
        InitWall();
    }
    private void InitWall()
    {
        _camera = Camera.main;
        Vector3 leftPos = _camera.ViewportToWorldPoint(new Vector3(0, 0, 10));
        Vector3 rightPos = _camera.ViewportToWorldPoint(new Vector3(1, 0, 10));
        Vector3 leftTarget = _camera.ViewportToWorldPoint(new Vector3(0, 1, 10));
        Vector3 rightTarget = _camera.ViewportToWorldPoint(new Vector3(1, 1, 10));
        _halfWallWidth = _leftWall.GetHalfWidth();
        SetUpWall(_leftWall, leftPos, leftTarget);
        SetUpWall(_rightWall, rightPos, rightTarget);
    }
    private void SetUpWall(WallController wall, Vector3 start, Vector3 target)
    {
        wall.transform.position = start;
        wall.SetMovingTarget(target);
        wall.SetStartPos(start);
    }
    public void MoveWallUp()
    {
        _leftWall.SetIsMoving(true);
        _rightWall.SetIsMoving(true);
    }
    public void StopMovingWall()
    {
        _leftWall.SetIsMoving(false);
        _rightWall.SetIsMoving(false);
    }
    public void SetWallImage(Sprite img)
    {
        _leftWall.SetSpriteImage(img);
        _rightWall.SetSpriteImage(img);
    }
}
