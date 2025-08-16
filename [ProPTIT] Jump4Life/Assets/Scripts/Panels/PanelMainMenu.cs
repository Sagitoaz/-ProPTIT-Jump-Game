using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelMainMenu : PanelController
{
    [SerializeField] private TextMeshProUGUI _highScoreText;
    public override void OpenPanel()
    {
        base.OpenPanel();
        PanelManager.Instance.SetHighScoreTMP(_highScoreText);
    }
    public void OnClickPlayButton()
    {
        GameManager.Instance.StartGame();
    }
}
