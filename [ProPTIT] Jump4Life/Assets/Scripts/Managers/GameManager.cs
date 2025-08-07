using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Vector3[] _paddlePos = {new Vector3(0.5f, 0.8f, 10),
                                    new Vector3(0.5f, 0.5f, 10),
                                    new Vector3(0.5f, 0.2f, 10)};
    private List<PaddleController> _paddleManager = new List<PaddleController>();
    private int _paddleCount;
    [SerializeField] private int _score = 0;
    private Camera _camera;
    [SerializeField] private float _translatePaddleSpeed = 5.0f;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _camera = Camera.main;
        _paddleCount = _paddlePos.Length;
        ChangePosToWorldPos();
    }
    private void Start()
    {
        InitPaddle();
    }
    public void IncreaseScore(int value)
    {
        _score += value;
    }
    private void ChangePosToWorldPos()
    {
        for (int i = 0; i < _paddleCount; i++)
        {
            _paddlePos[i] = _camera.ViewportToWorldPoint(_paddlePos[i]);
        }
    }
    private void InitPaddle()
    {
        for (int i = 0; i < _paddleCount; i++)
        {
            _paddleManager.Add(PaddlePool.Instance.GetPaddleFromPool());
            _paddleManager[i].transform.position = _paddlePos[i] + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
        }
    }
    public void MovePaddleToTop(PaddleController paddle)
    {
        int numberOfPaddleToDestroy = _paddleManager.IndexOf(paddle);
        Debug.Log("Cham be so: " + _paddleManager.IndexOf(paddle));
        for (int i = 0; i < numberOfPaddleToDestroy; i++)
        {
            PaddlePool.Instance.ReturnPaddleToPool(_paddleManager[0]);
            _paddleManager.RemoveAt(0);
        }
        for (int i = 0; i < numberOfPaddleToDestroy; i++)
        {
            PaddleController newPaddle = PaddlePool.Instance.GetPaddleFromPool();
            newPaddle.gameObject.SetActive(false);
            _paddleManager.Add(newPaddle);
        }
        for (int i = 0; i < _paddleCount; i++)
        {
            StartCoroutine(MovePaddleRoutine(_paddleManager[i], i));
        }
        
    }
    private IEnumerator MovePaddleRoutine(PaddleController paddle, int indexOfPos)
    {
        while (Vector3.Distance(paddle.transform.position, _paddlePos[indexOfPos]) > 0.1f)
        {
            paddle.transform.position = Vector3.MoveTowards(paddle.transform.position, _paddlePos[indexOfPos], _translatePaddleSpeed * Time.deltaTime);
            yield return null;
        }
        paddle.transform.position = _paddlePos[indexOfPos];
        paddle.gameObject.SetActive(true);
        paddle.SetIsTrigger(false);
    }
}
