using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelMainMenu : PanelController
{
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private Image _characterImage;
    private void Update()
    {
        if (_characterImage.sprite != GameManager.Instance.GetCurrentCharacterSprite())
        {
            SetCharacterImage(GameManager.Instance.GetCurrentCharacterSprite());
            _characterImage.preserveAspect = true;
        }
    }
    public override void OpenPanel()
    {
        base.OpenPanel();
        PanelManager.Instance.SetHighScoreTMP(_highScoreText);
    }
    public void OnClickPlayButton()
    {
        GameManager.Instance.StartGame();
    }
    public void OnClickSettingButton()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_SETTING);
    }
    public void OnClickCustomButton()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_CUSTOM);
    }
    public void SetCharacterImage(Sprite characterSprite)
    {
        _characterImage.sprite = characterSprite;
    }
}
