using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _jumpForce = 10f;
    private float _deathHeight = -15f;
    
    [Header("Debug")]
    [SerializeField] private bool _onPaddle = false;
    
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
    }

    private void InitializeComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
        _deathHeight = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
    }

    private void CheckPlayerInput()
    {
        bool jumpInput = GetJumpInput();
        
        if (jumpInput && _onPaddle)
        {
            Jump();
        }
    }

    private bool GetJumpInput()
    {
        // Touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            return true;
            
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
            RestartGame();
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

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        HandlePaddleExit();
    }

    private void HandlePaddleLanding(GameObject paddleObject)
    {
        _onPaddle = true;
        _playerAnim.SetTrigger(GameConfig.PLAYER_IDLE);
        _currentPaddle = paddleObject.GetComponent<PaddleController>();
        
        _currentPaddle.SetCanDamage(true);
        
        transform.SetParent(paddleObject.transform);

        if (!_currentPaddle.IsFirstPaddle() && Mathf.Abs(_currentPaddle.transform.position.x - transform.position.x) < 0.25f)
        {
            PanelManager.Instance.CreatePerfect(new Vector3(0, transform.position.y, 0));
            GameManager.Instance.IncreaseScore(1);
        }
        
        GameManager.Instance.IncreaseScore(1);
        PaddlesManager.Instance.MovePaddleToTop(_currentPaddle);
    }

    private void HandlePaddleExit()
    {
        _onPaddle = false;
        _playerAnim.SetTrigger(GameConfig.PLAYER_JUMP);
        FLipPlayer();
        if (_currentPaddle != null)
        {
            SoundManager.Instance.PlayJumpSound();
            _currentPaddle.SetIsTrigger(true);
            _currentPaddle.SetCanDamage(false);
        }
        
        transform.SetParent(null);
    }
}
