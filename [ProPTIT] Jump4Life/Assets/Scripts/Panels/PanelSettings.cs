using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelSettings : PanelController
{
    [SerializeField] private Sprite[] _soundSprite;
    [SerializeField] private Image _musicImg;
    [SerializeField] private Image _sfxImg;
    private void Update()
    {
        _musicImg.sprite = _soundSprite[SoundManager.Instance.GetMusicActive()];
        _sfxImg.sprite = _soundSprite[SoundManager.Instance.GetSFXActive()];
    }
    public void OnClickMusicButton()
    {
        SoundManager.Instance.ChangeActiveBGMusic();
    }
    public void OnClickSFXButton()
    {
        SoundManager.Instance.ChangeActiveSFX();
    }
    public void OnClickExitButton()
    {
        ClosePanel();
    }
}
