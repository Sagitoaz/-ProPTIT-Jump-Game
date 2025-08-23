using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPauseMenu : PanelController
{
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _pauseBoard;
    [SerializeField] private Sprite[] _soundSprite;
    [SerializeField] private Image _musicImg;
    [SerializeField] private Image _sfxImg;
    private void Awake()
    {
        _pauseBoard.SetActive(false);
    }
    private void Update()
    {
        _musicImg.sprite = _soundSprite[SoundManager.Instance.GetMusicActive()];
        _sfxImg.sprite = _soundSprite[SoundManager.Instance.GetSFXActive()];
    }
    public void OnClickPauseButton()
    {
        _pauseBoard.SetActive(true);
        _pauseButton.SetActive(false);
        GameManager.Instance.PauseGame();
    }
    public void OnClickExitButton()
    {
        _pauseBoard.SetActive(false);
        _pauseButton.SetActive(true);
        GameManager.Instance.ResumeGame();
    }
    public void OnClickHomeButton()
    {
        GameManager.Instance.RestartGame();
    }
    public void OnClickRestartButton()
    {
        GameManager.Instance.ReplayGame();
        GameManager.Instance.RestartGame();
    }
    public void OnClickMusicButton()
    {
        SoundManager.Instance.ChangeActiveBGMusic();
    }
    public void OnClickSFXButton()
    {
        SoundManager.Instance.ChangeActiveSFX();
    }
}
