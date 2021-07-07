using System;
using System.Collections;
using UnityEngine;

public class PolicemanVoice : MonoBehaviour
{
    [SerializeField] private AudioSource _policemanAudioSourse;
    [SerializeField] private AudioClip[] _policemanPhrases;

    private Policeman _policeman;
    private int _currentSoundIndex;

    private IEnumerator _chaseCoroutine;
    
    private void Start()
    {
        _policeman = GetComponent<Policeman>();
        _policeman.ChaseStart += StartScream;
        _policeman.ChaseEnd += EndScream;
    }

    private void StartScream()
    {
        if (_chaseCoroutine == null)
        {
            _chaseCoroutine = ChaseScream();
            StartCoroutine(_chaseCoroutine);
        }
    }

    private void EndScream()
    {
        StopCoroutine(_chaseCoroutine);
        _chaseCoroutine = null;
    }
    
    private IEnumerator ChaseScream()
    {
        while (true)
        {
            if (_policemanAudioSourse.isPlaying == false)
            {
                SwitchSound();
            }
            yield return new WaitForSeconds(_policemanAudioSourse.clip.length);
            yield return new WaitForSeconds(7f);
        }
    }
    
    private void SwitchSound()
    {
        
        if (_currentSoundIndex == _policemanPhrases.Length)
        {
            _currentSoundIndex = 0;
        }
        else
        {
            _currentSoundIndex++;
        }
        
        _policemanAudioSourse.clip = _policemanPhrases[_currentSoundIndex];
        _policemanAudioSourse.Play();
    }
}
