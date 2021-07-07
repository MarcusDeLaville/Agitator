using UnityEngine;

public class VoiceProtest : MonoBehaviour
{

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip[] _megafonPlayList;
    [SerializeField] private int _currentSoundIndex = 0;

    private void Update()
    {
        if (_audioSource.isPlaying == false)
        {
            SwitchSound();
        }
    }

    private void SwitchSound()
    {
        if (_currentSoundIndex == _megafonPlayList.Length - 1)
        {
            _currentSoundIndex = 0;
        }
        else
        {
            _currentSoundIndex++;
        }
        
        _audioSource.clip = _megafonPlayList[_currentSoundIndex];
        _audioSource.Play();
    }
}
