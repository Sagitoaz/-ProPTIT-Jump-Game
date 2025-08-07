using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePool : MonoBehaviour
{
    public static PaddlePool Instance;
    [SerializeField] private PaddleController _paddlePrefabs;
    [SerializeField] private List<PaddleController> _paddlePool = new List<PaddleController>();
    private int _paddlePoolSize = 10;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Init();
    }
    private void Init()
    {
        for (int idx = 0; idx < _paddlePoolSize; idx++)
        {
            PaddleController newPaddle = Instantiate(_paddlePrefabs, transform.position, Quaternion.identity);
            newPaddle.gameObject.SetActive(false);
            _paddlePool.Add(newPaddle);
        }
    }
    public PaddleController GetPaddleFromPool()
    {
        for (int idx = 0; idx < _paddlePool.Count; idx++)
        {
            if (!_paddlePool[idx].gameObject.activeSelf)
            {
                _paddlePool[idx].gameObject.SetActive(true);
                return _paddlePool[idx];
            }
        }
        _paddlePoolSize++;
        PaddleController newPaddle = Instantiate(_paddlePrefabs, transform.position, Quaternion.identity);
        _paddlePool.Add(newPaddle);
        return newPaddle;
    }
    public void ReturnPaddleToPool(PaddleController usedPaddle)
    {
        usedPaddle.gameObject.SetActive(false);
    }
}
