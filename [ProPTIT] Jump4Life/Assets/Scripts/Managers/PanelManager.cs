using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    private Dictionary<string, PanelController> _panelDict = new Dictionary<string, PanelController>();
    [Header("Panel elements")]
    [SerializeField] private PanelController _ingamePanel;
    [SerializeField] private PanelController _mainMenuPanel;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _perfectPopUp;
    [SerializeField] private Transform _popupCanvas;
    public override void Awake()
    {
        base.Awake();
        OpenPanel(GameConfig.PANEL_MAINMENU);
    }
    private void Start()
    {
        var panelList = GetComponentsInChildren<PanelController>();
        foreach (PanelController panel in panelList)
        {
            _panelDict[panel.name] = panel;
        }
        ShowHighScoreText(GameManager.Instance.GetHighScore());
    }

    // MANAGE PANEL IN CANVAS - UI

    public PanelController GetPanel(string panelName)
    {
        if (IsExistedInDict(panelName))
        {
            return _panelDict[panelName];
        }
        PanelController panel = Resources.Load<PanelController>(GameConfig.UI_PATH + panelName);
        Debug.Log(GameConfig.UI_PATH + panelName);
        PanelController newPanel = Instantiate(panel, transform);
        newPanel.transform.SetAsLastSibling();
        newPanel.name = panelName;
        _panelDict[name] = newPanel;
        return newPanel;
    }
    public void RemovePanel(string panelName)
    {
        if (IsExistedInDict(panelName))
        {
            _panelDict.Remove(panelName);
        }
    }
    public void OpenPanel(string panelName)
    {
        PanelController panel = GetPanel(panelName);
        panel.OpenPanel();
    }
    public void ClosePanel(string panelName)
    {
        PanelController panel = GetPanel(panelName);
        panel.ClosePanel();
    }
    public void CloseAllPanel()
    {
        foreach (PanelController panel in _panelDict.Values)
        {
            panel.ClosePanel();
        }
    }
    private bool IsExistedInDict(string panelName)
    {
        return _panelDict.ContainsKey(panelName);
    }

    // METHOD TO CALL

    public void SetHighScoreTMP(TextMeshProUGUI tmp)
    {
        _highScoreText = tmp;
    }
    public void SetScoreTMP(TextMeshProUGUI tmp)
    {
        _scoreText = tmp;
    }
    private void ShowHighScoreText(int highScore)
    {
        _highScoreText.text = highScore.ToString();
    }
    public void UpdateScore(int value)
    {
        if (_scoreText != null)
        {
            _scoreText.text = "" + value;
        }
    }
    public void CreatePerfect(Vector3 pos)
    {
        TextMeshProUGUI perfect = Instantiate(_perfectPopUp, pos, Quaternion.identity, _popupCanvas);
        Destroy(perfect, 1f);
    }
}
