using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _breakSound;
    public void PlayJumpSound()
    {
        _audioSource.PlayOneShot(_jumpSound);
    }
    public void PlayBreakSound()
    {
        _audioSource.PlayOneShot(_breakSound);
    }
}
