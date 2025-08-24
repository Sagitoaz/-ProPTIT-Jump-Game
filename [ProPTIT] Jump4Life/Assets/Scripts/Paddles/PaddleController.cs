using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleController : MonoBehaviour
{
    protected BoxCollider2D _boxCollider;
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected float _speed = 1.5f;
    protected float _screenLeft, _screenRight;
    protected Camera _camera;
    protected Vector2 _direction = Vector2.zero;
    protected float _halfPaddleSize;
    [SerializeField] protected int _health = 2;
    [SerializeField] protected Sprite[] _spriteImgs;
    protected abstract void CalculateMovement();
    protected abstract void Damage(int dmg);
    [SerializeField] protected bool _canDamage = false;
    [SerializeField] protected bool _isFirstPaddle = false;
    protected void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
        _halfPaddleSize = GetComponent<SpriteRenderer>().bounds.extents.x;
        _screenLeft = _camera.ViewportToWorldPoint(Vector3.zero).x + _halfPaddleSize + WallManager.Instance._halfWallWidth;
        _screenRight = _camera.ViewportToWorldPoint(Vector3.right).x - _halfPaddleSize - WallManager.Instance._halfWallWidth;
        Init();
    }
    private void Update()
    {
        if (!PaddlesManager.Instance._isSetUpDone) return;
        
        if (_isFirstPaddle)
        {
            if (Mathf.Abs(transform.position.x) > 0.01f)
            {
                Vector3 targetPos = new Vector3(0, transform.position.y, transform.position.z);
                transform.position = targetPos;
            }
            return;
        }
        CalculateMovement();
    }
    private void Init()
    {
        if (!_isFirstPaddle)
        {
            _direction = (Random.value < 0.5) ? Vector2.left : Vector2.right;
        }
    }
    protected void DestroyPaddle()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag(GameConfig.PLAYER_TAG))
                {
                    child.SetParent(null);
                }
            }
        }
        PaddlePool.Instance.ReturnPaddleToPool(this);
    }
    public void ResetPaddle()
    {
        _health = 2;
        _spriteRenderer.sprite = _spriteImgs[0];
        _canDamage = false;
        _isFirstPaddle = false;
    }
    public void SetIsTrigger(bool isTrigger)
    {
        _boxCollider.isTrigger = isTrigger;
    }
    protected IEnumerator DamageCoolDown(float seconds)
    {
        _canDamage = false;
        yield return new WaitForSeconds(seconds);
        _canDamage = true;
    }
    public void SetCanDamage(bool value)
    {
        _canDamage = value;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void SetAsFirstPaddle(bool isFirst)
    {
        _isFirstPaddle = isFirst;
    }
    
    public bool IsFirstPaddle()
    {
        return _isFirstPaddle;
    }
}