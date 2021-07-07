using System;
using System.Collections;
using UnityEngine;

public class EngineSound : MonoBehaviour 
{
    [Header("Driver"), SerializeField] private InteractionCar _interaction;
    [SerializeField] private AudioSource _engineAudio;
    [SerializeField] private AudioSource _brakeSounds;

    [SerializeField] private AudioClip _start;
    [SerializeField] private AudioClip _normal;
    [SerializeField] private AudioClip _acceleration;
    [SerializeField] private AudioClip _brake;

    [SerializeField] private CarMovenment _carMovenment;

    private bool _moveStarted = false;
    private bool _moveCanseled = false;

    private bool _engineWork;
    private bool _isBraking;
    
    private void EnablePower()
    {
        StartCoroutine(StartEngine());
    }
    
    private void Update()
    {
        if (_engineWork == true)
        {
            if(Input.GetAxis("Vertical") != 0f)
            {
                if (_moveStarted == false)
                {
                    OnMoveStarted();
                }

                // _engineAudio.clip = _acceleration;
            }
            else
            {
                // _engineAudio.clip = _normal;
                if (_moveCanseled == false)
                {
                    OnMoveCanceled();
                }
            }
        }
        
        if(_carMovenment.IsBraking == true)
        {
            if (_isBraking == false)
            {
                StartCoroutine(StartBraking());
            }
        }
        else
        {
            _brakeSounds.Stop();
        }
    }

    private IEnumerator StartBraking()
    {
        _isBraking = true;
        _brakeSounds.clip = _brake;
        _brakeSounds.Play();
        yield return new WaitForSeconds(_brakeSounds.clip.length);
        _isBraking = false;
    }
    
    private IEnumerator StartEngine()
    {
        _engineAudio.clip = _start;
        _engineAudio.Play();
        yield return new WaitForSeconds(_engineAudio.clip.length);
        _engineAudio.clip = _normal;
        _engineWork = true;
    }

    private void OnEnable()
    {
        _interaction.CarPowerOn += EnablePower;
        _interaction.PlayerQuit += OnEscapeFromCar;
    }

    private void OnDisable()
    {
        _interaction.CarPowerOn -= EnablePower;
        _interaction.PlayerQuit -= OnEscapeFromCar;
    }

    private void OnMoveStarted()
    {
        _moveStarted = true;
        _moveCanseled = false;
        _engineAudio.clip = _acceleration;
        _engineAudio.Play();
        // Debug.Log("acceration");
    }

    private void OnMoveCanceled()
    {
        _moveCanseled = true;
        _moveStarted = false;
        _engineAudio.clip = _normal;
        _engineAudio.Play();
        // Debug.Log("normal");
    }

    private void OnEscapeFromCar()
    {
        _engineWork = false;
        StartCoroutine(StartBraking());
        _engineAudio.Stop();
    }
}