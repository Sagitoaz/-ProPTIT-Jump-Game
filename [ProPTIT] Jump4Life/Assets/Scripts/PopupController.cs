using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] private AnimationCurve _opacityCurve;
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private AnimationCurve _heightCurve;
    private Vector3 _originPos;
    private TextMeshProUGUI _perfectPopUp;
    private float time = 0;
    private void Awake()
    {
        _perfectPopUp = GetComponent<TextMeshProUGUI>();
        _originPos = transform.position;
    }
    private void Update()
    {
        _perfectPopUp.color = new Color(1, 1, 1, _opacityCurve.Evaluate(time));
        transform.localScale = Vector3.one * _scaleCurve.Evaluate(time);
        transform.position = _originPos + new Vector3(0, 1 + _heightCurve.Evaluate(time), 0);
        time += Time.deltaTime;
    }
}
