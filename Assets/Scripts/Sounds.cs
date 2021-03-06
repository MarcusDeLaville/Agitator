using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;

    public bool _doSound { get; private set; } = true;

    private void Awake()
    {
        //_audioSources = FindObjectsOfType<AudioSource>();

        if (!PlayerPrefs.HasKey("Sounds"))
        {
            PlayerPrefs.SetInt("Sounds", 1);
        }

        GetSoundSettings();
    }

    private void Start()
    {
        SetSoundsVolume();
    }

    private void SetSoundsVolume()
    {
        for (int i = 0; i < _audioSources.Length; i++)
        {
            if (_doSound)
            {
                _audioSources[i].volume = 1;
            }
            else if (!_doSound)
            {
                _audioSources[i].volume = 0;
            }
        }
    }

    public void SwitchSoundState()
    {
        _doSound = !_doSound;
        SaveSoundSettings();

        SetSoundsVolume();
    }

    public void TapSound(AudioClip audioClip)
    {
        _audioSources[0].clip = audioClip;
        _audioSources[0].Play();
    }

    public void EffectSound(AudioClip audioClip)
    {
        _audioSources[1].clip = audioClip;
        _audioSources[1].Play();
    }

    private void SaveSoundSettings()
    {
        if (_doSound)
        {
            PlayerPrefs.SetInt("Sounds", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sounds", 0);
        }
    }

    private void GetSoundSettings()
    {
        if (PlayerPrefs.GetInt("Sounds") == 1)
        {
            _doSound = true;
        }
        else if (PlayerPrefs.GetInt("Sounds") == 0)
        {
            _doSound = false;
        }

    }
}

