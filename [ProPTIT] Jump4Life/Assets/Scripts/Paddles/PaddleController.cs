using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleController : MonoBehaviour
{
    protected BoxCollider2D _boxCollider;
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected float _speed = 3.0f;
    protected float _screenLeft, _screenRight;
    protected Camera _camera;
    protected Vector2 _direction;
    protected float _halfPaddleSize;
    [SerializeField] protected int _health = 2;
    [SerializeField] protected Sprite[] _spriteImgs;
    protected abstract void CalculateMovement();
    protected abstract void Damage(int dmg);
    [SerializeField] protected bool _canDamage = false;
    protected void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
        _halfPaddleSize = GetComponent<SpriteRenderer>().bounds.extents.x;
        _screenLeft = _camera.ViewportToWorldPoint(Vector3.zero).x + _halfPaddleSize;
        _screenRight = _camera.ViewportToWorldPoint(Vector3.right).x - _halfPaddleSize;
        Init();
    }
    private void Update()
    {
        CalculateMovement();
    }
    private void Init()
    {
        _direction = (Random.value < 0.5) ? Vector2.left : Vector2.right;
    }
    protected void DestroyPaddle()
    {
        // PaddlesManager.Instance._paddleManager.Remove(this);
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
    }
    public void SetIsTrigger(bool isTrigger)
    {
        _boxCollider.isTrigger = isTrigger;
    }
    protected IEnumerator DamageCoolDown(float seconds)
    {
        Debug.Log("Start cooldown on " + gameObject.name);
        _canDamage = false;
        yield return new WaitForSeconds(seconds);
        _canDamage = true;
        Debug.Log("Cooldown complete on " + gameObject.name);
    }
    public void SetCanDamage(bool value)
    {
        _canDamage = value;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}