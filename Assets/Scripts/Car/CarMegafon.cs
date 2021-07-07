using UnityEngine;

public class CarMegafon : Megafon
{
    [SerializeField] private GameObject _zoneInteraction;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip[] _megafonPlayList;
    [SerializeField] private int _currentSoundIndex = 0;
    
    private bool _isActivate = false;

    public bool IsActivate => _isActivate;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(_isActivate == false)
            {
                Enable();
                _isActivate = true;
            }
            else
            {
                Disable();
                _isActivate = false;
            }
        }

        if (_isActivate == true)
        {
            if (_audioSource.isPlaying == false)
            {
                SwitchSound();
            }
        }
    }

    private void Enable()
    {
        SwitchMegafonStatus(true);
    }

    private void Disable()
    {
        SwitchMegafonStatus(false);
    }

    private void SwitchMegafonStatus(bool status)
    {
        _zoneInteraction.SetActive(status);

        if(status == true)
        {
            
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void SwitchSound()
    {
        if (_currentSoundIndex == _megafonPlayList.Length)
        {
            _currentSoundIndex = 0;
        }
        
        _audioSource.clip = _megafonPlayList[_currentSoundIndex];
        _audioSource.Play();
        
        if(_currentSoundIndex < _megafonPlayList.Length)
        {
            _currentSoundIndex++;
        }
        
    }
}