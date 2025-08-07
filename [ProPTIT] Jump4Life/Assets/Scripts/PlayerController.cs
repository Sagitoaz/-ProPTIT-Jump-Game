using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private bool _onPaddle = false;
    private PaddleController _currentPaddle;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CalculateMovement();
    }
    private void CalculateMovement()
    {
        if (transform.position.x < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (_onPaddle)
            {
                Jump();
            }
        }
    }
    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameConfig.PADDLE_TAG))
        {
            _onPaddle = true;
            _currentPaddle = other.gameObject.GetComponent<PaddleController>();
            _currentPaddle.SetCanDamage(true);
            transform.SetParent(other.transform);
            GameManager.Instance.IncreaseScore(1);
            GameManager.Instance.MovePaddleToTop(_currentPaddle);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameConfig.PADDLE_TAG))
        {
            _onPaddle = false;
            _currentPaddle.SetIsTrigger(true);
            _currentPaddle.SetCanDamage(false);
            transform.SetParent(null);
        }
    }
}
