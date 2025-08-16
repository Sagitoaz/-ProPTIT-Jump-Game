using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelIngame : PanelController
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    public override void OpenPanel()
    {
        base.OpenPanel();
        PanelManager.Instance.SetScoreTMP(_scoreText);
    }
}
