using System;
using UnityEngine;

public class BumTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _bumAudioSourse;
    [SerializeField] private AudioClip[] _bumPhrase;

    [SerializeField] private LayerMask _playerLayer;
    
    [SerializeField] private int _currentPhraseIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _playerLayer) != 0)
        {
            SayPhrase();
        }
    }

    private void SayPhrase()
    {
        if (_currentPhraseIndex == _bumPhrase.Length)
        {
            _currentPhraseIndex = 0;
        }
        
        _bumAudioSourse.clip = _bumPhrase[_currentPhraseIndex];
        _bumAudioSourse.Play();

        if(_currentPhraseIndex < _bumPhrase.Length)
        {
            _currentPhraseIndex++;
        }
    }
}
