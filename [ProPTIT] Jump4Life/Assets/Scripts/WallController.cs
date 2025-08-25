using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private float _halfWallWidth;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool _isMoving = false;
    private Vector3 _movingTarget = Vector3.zero;
    private Vector3 _startPos = Vector3.zero;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _halfWallWidth = _spriteRenderer.bounds.extents.x;
    }
    private void Update()
    {
        CalculateMovement();
    }
    private void CalculateMovement()
    {
        if (!_isMoving) return;
        if (transform.position.y >= _movingTarget.y)
        {
            transform.position = _startPos;
        }
        transform.position = Vector3.MoveTowards(transform.position, _movingTarget, _speed * Time.deltaTime);
    }
    public void SetMovingTarget(Vector3 target)
    {
        _movingTarget = target;
    }
    public float GetHalfWidth()
    {
        return _halfWallWidth;
    }
    public void SetIsMoving(bool isMove)
    {
        _isMoving = isMove;
    }
    public void SetStartPos(Vector3 start)
    {
        _startPos = start;
    }
    public void SetSpriteImage(Sprite img)
    {
        _spriteRenderer.sprite = img;
    }
}
