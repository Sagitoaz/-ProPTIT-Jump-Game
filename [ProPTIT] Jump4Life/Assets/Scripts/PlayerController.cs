using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _deathHeight = -15f;
    
    [Header("Debug")]
    [SerializeField] private bool _onPaddle = false;
    
    private Rigidbody2D _rb;
    private PaddleController _currentPaddle;

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
        _currentPaddle = paddleObject.GetComponent<PaddleController>();
        
        // Set paddle properties
        _currentPaddle.SetCanDamage(true);
        
        // Attach player to paddle
        transform.SetParent(paddleObject.transform);
        
        // Update game state
        GameManager.Instance.IncreaseScore(1);
        PaddlesManager.Instance.MovePaddleToTop(_currentPaddle);
    }

    private void HandlePaddleExit()
    {
        _onPaddle = false;
        
        if (_currentPaddle != null)
        {
            _currentPaddle.SetIsTrigger(true);
            _currentPaddle.SetCanDamage(false);
        }
        
        // Detach from paddle
        transform.SetParent(null);
    }
}
