using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _breakSound;
    [SerializeField] private bool _activeSFX;
    [SerializeField] private bool _activeBGMusic;
    public override void Awake()
    {
        base.Awake();
        _activeBGMusic = true;
        _activeSFX = true;
    }
    public void PlayJumpSound()
    {
        PlayAudioClip(_jumpSound);
    }
    public void PlayBreakSound()
    {
        PlayAudioClip(_breakSound);
    }
    public void ChangeActiveBGMusic()
    {
        _activeBGMusic = !_activeBGMusic;
        if (!_activeBGMusic)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.Play();
        }
    }
    public void ChangeActiveSFX()
    {
        _activeSFX = !_activeSFX;
    }
    private void PlayAudioClip(AudioClip audio)
    {
        if (_activeSFX && audio != null)
        {
            _audioSource.PlayOneShot(audio);
        }
    }
}
