using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private AudioSource _audioSource;

    private int _clipIndex = 0;
    private bool _isActive = false;

    private void Start()
    {
        _audioSource.enabled = false;
        _audioSource.clip = _sounds[_clipIndex];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SwitchRadioCondition();
        }

        if (_audioSource.enabled == true)
        {
            if (_audioSource.isPlaying == false)
            {
                PickNextClip();
                _audioSource.Play();
            }
        }
    }

    private void SwitchRadioCondition()
    {
        _isActive = !_isActive;

        _audioSource.enabled = _isActive; 
        if(_isActive == true)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.Play();
        }
    }

    private void PickNextClip()
    {
        _clipIndex++;

        if (_clipIndex >= _sounds.Length)
        {
            _clipIndex = 0;
        }

        _audioSource.clip = _sounds[_clipIndex];
    }
}
