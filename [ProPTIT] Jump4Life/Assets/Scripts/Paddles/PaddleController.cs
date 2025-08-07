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
    [SerializeField] protected int _health = 3;
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
        // Debug.Log("Destroy Paddle");
    }
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag(GameConfig.PLAYER_TAG))
    //     {
    //         DestroyPaddle();
    //     }
    // }
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
}