using System;
using System.Collections;
using UnityEngine;

public class EngineSounds : MonoBehaviour
{
    [Header("Driver"), SerializeField] private InteractionCar _interaction;
    [SerializeField] private AudioSource _engineAudio;

    [Header("Idle Pitch value"),SerializeField] private float _minPitchValue = 0.3f;
    [Header("Max Pitch value"), SerializeField] private float _maxPitchValue = 2.2f;
    [Header("Change Speed duration"), Range(0.01f, 0.1f), SerializeField] private float _durationSpeed = 0.03f;

    [SerializeField] private AudioClip _start;
    [SerializeField] private AudioClip _normal;
    [SerializeField] private AudioClip _acceleration;
    [SerializeField] private AudioClip _brake;
    
    private readonly WaitForEndOfFrame _wait = new WaitForEndOfFrame();
    private Coroutine _coroutine;
    private Coroutine _coroutine2;
    private float _currentPitchLevel;

    private bool _moveStarted = false;
    private bool _moveCanseled = false;

    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0f)
        {
            if (_moveStarted == false)
            {
                OnMoveStarted();
            }
        }
        else
        {
            if (_moveCanseled == false)
            {
                OnMoveCanceled();
            }
        }
    }

    private void OnValidate()
    {
        if (_minPitchValue >= _maxPitchValue)
            _minPitchValue = _maxPitchValue - 0.1f;
        if (_maxPitchValue <= _minPitchValue)
            _maxPitchValue = _minPitchValue + 0.1f;
    }

    private void OnEnable()
    {
        _interaction.PlayerQuit += OnEscapeFromCar;

        _engineAudio.pitch = _minPitchValue;
        _engineAudio.loop = true;
        _currentPitchLevel = _minPitchValue;
    }

    private void OnDisable()
    {
        _interaction.PlayerQuit -= OnEscapeFromCar;
    }

    private void OnMoveStarted()
    {
        _moveStarted = true;
        _moveCanseled = false;
        if (_coroutine2 != null)
            StopCoroutine(_coroutine2);

        _coroutine = StartCoroutine(LerpPitchLevel(_currentPitchLevel, _maxPitchValue));
    }

    private void OnMoveCanceled()
    {
        _moveCanseled = true;
        _moveStarted = false;
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine2 = StartCoroutine(LerpPitchLevel(_currentPitchLevel, _minPitchValue));
    }

    private IEnumerator LerpPitchLevel(float startValue, float endValue)
    {
        if (startValue < endValue)
        {
            while (_currentPitchLevel <= endValue)
            {
                _currentPitchLevel = (float)System.Math.Round(_currentPitchLevel + _durationSpeed, 2);
                _engineAudio.pitch = _currentPitchLevel;
                yield return _wait;
            }
        }
        else
        {
            while (_currentPitchLevel >= endValue)
            {
                _currentPitchLevel = (float)System.Math.Round(_currentPitchLevel - _durationSpeed, 2);
                _engineAudio.pitch = _currentPitchLevel;
                yield return _wait;
            }
        }
        
        
    }

    private void OnEscapeFromCar()
    {
        // Input.Engine.Move.Disable();
    }
}