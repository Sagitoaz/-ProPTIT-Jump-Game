using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlesManager : Singleton<PaddlesManager>
{
    [Header("Paddle Configuration")]
    [SerializeField] private float _translatePaddleSpeed = 5.0f;
    [SerializeField]
    private Vector3[] _paddlePositions = {
        new Vector3(0.5f, 0.8f, 10),
        new Vector3(0.5f, 0.5f, 10),
        new Vector3(0.5f, 0.2f, 10),
        new Vector3(0.5f, -0.1f, 10),
        new Vector3(0.5f, -0.4f, 10)
    };

    [Header("Debug")]
    public List<PaddleController> _paddleManager = new List<PaddleController>();

    private Camera _camera;
    private int _paddleCount;
    public bool _isSetUpDone = false;

    public override void Awake()
    {
        base.Awake();
        InitializeComponents();
    }

    private void Start()
    {
        InitializePaddles();
        _isSetUpDone = true;
    }

    private void InitializeComponents()
    {
        _camera = Camera.main;
        _paddleCount = _paddlePositions.Length;
        ConvertToWorldPositions();
    }

    private void ConvertToWorldPositions()
    {
        for (int i = 0; i < _paddleCount; i++)
        {
            _paddlePositions[i] = _camera.ViewportToWorldPoint(_paddlePositions[i]);
        }
    }

    private void InitializePaddles()
    {
        for (int i = 0; i < _paddleCount; i++)
        {
            CreatePaddleAtPosition(i);
        }
        _paddleManager[0].transform.position = _paddlePositions[0];
        _paddleManager[0].SetAsFirstPaddle(true);
    }

    private void CreatePaddleAtPosition(int index)
    {
        PaddleController paddle = PaddlePool.Instance.GetPaddleFromPool();
        Vector3 randomOffset = new Vector3(Random.Range(-1.5f, 1.5f), 0, 0);
        paddle.transform.position = _paddlePositions[index] + randomOffset;
        _paddleManager.Add(paddle);
    }

    public void MovePaddleToTop(PaddleController currentPaddle)
    {
        RemoveBottomPaddles(currentPaddle);
        AddNewPaddles();
        RepositionAllPaddles();
    }

    private void RemoveBottomPaddles(PaddleController currentPaddle)
    {
        while (_paddleManager.IndexOf(currentPaddle) > 0)
        {
            GameManager.Instance.IncreaseScore(1);
            ReturnPaddleToPool(_paddleManager[0]);
            _paddleManager.RemoveAt(0);
            AddNewPaddleToTop();
        }
    }

    private void AddNewPaddles()
    {
        while (_paddleManager.Count < _paddleCount)
        {
            AddNewPaddleToTop();
        }
    }

    private void AddNewPaddleToTop()
    {
        PaddleController newPaddle = PaddlePool.Instance.GetPaddleFromPool();
        _paddleManager.Add(newPaddle);
    }

    private void ReturnPaddleToPool(PaddleController paddle)
    {
        PaddlePool.Instance.ReturnPaddleToPool(paddle);
    }

    private void RepositionAllPaddles()
    {
        for (int i = 0; i < _paddleManager.Count; i++)
        {
            StartCoroutine(MovePaddleToPosition(_paddleManager[i], i));
        }
    }

    private IEnumerator MovePaddleToPosition(PaddleController paddle, int targetIndex)
    {
        if (targetIndex == 0 && paddle.IsFirstPaddle())
        {
            paddle.transform.position = _paddlePositions[0];
            ConfigurePaddleAtIndex(paddle, targetIndex);
            yield break;
        }

        Vector3 randomOffset = new Vector3(Random.Range(-1.5f, 1.5f), 0, 0);
        Vector3 targetPosition = _paddlePositions[targetIndex] + randomOffset;
        if (targetIndex == 0)
        {
            WallManager.Instance.MoveWallUp();
        }
        // Move paddle to target position
        while (Vector3.Distance(paddle.transform.position, targetPosition) > 0.1f)
        {
            paddle.transform.position = Vector3.MoveTowards(paddle.transform.position, targetPosition, _translatePaddleSpeed * Time.deltaTime);
            yield return null;
        }
        if (targetIndex == 0)
        {
            WallManager.Instance.StopMovingWall();
        }

        // Move paddle to exact position
        paddle.transform.position = targetPosition;

        // Configure paddle based on its position
        ConfigurePaddleAtIndex(paddle, targetIndex);
    }

    private void ConfigurePaddleAtIndex(PaddleController paddle, int index)
    {
        const int ACTIVE_PADDLE_COUNT = 3;

        if (index < ACTIVE_PADDLE_COUNT)
        {
            paddle.SetIsTrigger(false);
            paddle.gameObject.SetActive(true);
        }
        else
        {
            paddle.SetIsTrigger(true);
        }
    }
}
