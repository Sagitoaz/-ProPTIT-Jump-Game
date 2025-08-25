using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _jumpForce = 10f;
    private float _deathHeight = -15f;

    [Header("Debug")]
    [SerializeField] private bool _onPaddle = false;
    [SerializeField] private bool _canJump = true;
    [SerializeField] private float _jumpCoolDown = 0.5f;
    [SerializeField] private float _nextJump = 0f;

    private Rigidbody2D _rb;
    private PaddleController _currentPaddle;
    private Animator _playerAnim;

    private void Awake()
    {
        InitializeComponents();
    }

    private void Update()
    {
        CheckPlayerInput();
        CheckPlayerDeath();
        JumpCoolDown();
    }

    private void InitializeComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _deathHeight = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
    }

    private void CheckPlayerInput()
    {
        bool jumpInput = GetJumpInput();

        if (jumpInput && _onPaddle && _canJump)
        {
            Jump();
            _canJump = false;
            _nextJump = Time.time + _jumpCoolDown;
        }
    }

    private void JumpCoolDown()
    {
        if (Time.time >= _nextJump)
        {
            _canJump = true;
        }
    }

    private bool GetJumpInput()
    {
        // Touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                // Debug.Log("Touch: " + EventSystem.current != null);
                // Debug.Log("Touch 2: " + EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId));
                return false;
            }
            return true;
        }

        // Mouse input (for testing)
        if (Input.GetMouseButtonDown(0))
            return true;

        return false;
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }

    private void CheckPlayerDeath()
    {
        if (transform.position.y < _deathHeight)
        {
            GameManager.Instance.GameOver();
        }
    }
    private void FLipPlayer()
    {
        if (transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(GameConfig.PADDLE_TAG))
            return;

        Vector2 collisionNormal = collision.contacts[0].normal;

        // Only register collision if landing on top of paddle
        if (collisionNormal.y > 0.5f && _rb.velocity.y <= 0f)
        {
            HandlePaddleLanding(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(GameConfig.PADDLE_TAG))
            return;

        if (collision.gameObject != _currentPaddle.gameObject) return;

        if (!collision.gameObject.activeInHierarchy) return;

        HandlePaddleExit();
    }

    private void HandlePaddleLanding(GameObject paddleObject)
    {
        _onPaddle = true;
        if (_playerAnim != null)
        {
            _playerAnim.SetTrigger(GameConfig.PLAYER_IDLE);
        }
        _currentPaddle = paddleObject.GetComponent<PaddleController>();

        _currentPaddle.SetCanDamage(true);

        transform.SetParent(paddleObject.transform);

        if (!_currentPaddle.IsFirstPaddle() && Mathf.Abs(_currentPaddle.transform.position.x - transform.position.x) < 0.25f)
        {
            PanelManager.Instance.CreatePerfect(new Vector3(0, transform.position.y, 0));
            GameManager.Instance.IncreaseScore(1);
        }

        PaddlesManager.Instance.MovePaddleToTop(_currentPaddle);
    }

    private void HandlePaddleExit()
    {
        _onPaddle = false;
        if (_playerAnim != null)
        {
            _playerAnim.SetTrigger(GameConfig.PLAYER_JUMP);
        }
        FLipPlayer();
        if (_currentPaddle != null)
        {
            SoundManager.Instance.PlayJumpSound();
            _currentPaddle.SetIsTrigger(true);
            _currentPaddle.SetCanDamage(false);
        }

        transform.SetParent(null);
    }
    public void SetAnimator(Animator animator)
    {
        _playerAnim = animator;
    }
}
