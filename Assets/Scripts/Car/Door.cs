using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Door : InteractItem
{
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private Vector3 _openAngles = new Vector3(0, 90, 0);
    [SerializeField] private Vector3 _closeAngles = new Vector3(0, 0, 0);
    [SerializeField] private float _speed = 3;

    [SerializeField] private AudioSource _interactSound;
    [SerializeField] private AudioClip _openedSound;
    [SerializeField] private AudioClip _closedSound;

    private Vector3 _targetAngles = Vector3.zero;
    private Coroutine _rotating;

    protected Transform DoorTransform => _doorTransform;

    public event Action<PlayerStack> OnDoorOpened;
    
    public bool IsOpen { get; private set; } = false;

    private PlayerStack _playerStack;
    
    protected override void OnInteract(PlayerStack player)
    {
        _playerStack = player;
        
        if (_targetAngles == _openAngles)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Close()
    {
        SwitchDoorState(_closeAngles);
        PlayInteractableSound(_closedSound);
    }

    public void Open()
    {
        SwitchDoorState(_openAngles);
        PlayInteractableSound(_openedSound);
    }

    private void SwitchDoorState(Vector3 targerAngles)
    {
        _targetAngles = targerAngles;
        IsOpen = !IsOpen;

        if(_rotating != null)
        {
            StopCoroutine(_rotating);
        }

        _rotating = StartCoroutine(RotateDoor(targerAngles));
    }

    private IEnumerator RotateDoor(Vector3 targerAngles)
    {
        _doorTransform.DOLocalRotate(targerAngles, _speed, RotateMode.Fast);
        yield return null;
        yield return new WaitForSeconds(_speed);
        OnDoorOpened?.Invoke(_playerStack);
        // _playerStack = null;
    }

    private void PlayInteractableSound(AudioClip sound)
    {
        _interactSound.clip = sound;
        _interactSound.Play();
    }
}