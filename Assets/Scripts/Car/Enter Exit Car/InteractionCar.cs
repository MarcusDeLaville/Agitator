using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class InteractionCar : MonoBehaviour
{
    [SerializeField] private Transform _playerContainer;
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private CarStack _carStack;
    
    private PlayerStack _player;
    private bool _canSeat = true;

    public event Action PlayerEntered;
    public event Action CarPowerOn;
    public event Action PlayerQuit;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Escape();
        }
    }

    public void Enter(PlayerStack player)
    {
        if (_canSeat == true)
        {
            _canSeat = false;
            _player = player;
            
            _player.StandatdCamera.SetActive(false);
            _player.CarCamera.SetActive(true);

            var cinemachineVirtualCamera = _player.CarCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
            cinemachineVirtualCamera.m_FollowOffset = new Vector3(cinemachineVirtualCamera.m_FollowOffset.x, cinemachineVirtualCamera.m_FollowOffset.y, _carStack.CameraDistance);
            
            _player.PlayerMovenment.IsFreezeMoving = true;
            _player.Collider.enabled = false;

            _player.Rigidbody.isKinematic = true;
            _player.Rigidbody.detectCollisions = false;
            _player.Transform.SetParent(_playerContainer);
            _player.Transform.position = _playerContainer.position;
            _player.Transform.localEulerAngles = new Vector3(0, -180, 0);
            _player.Animator.SetBool("isDriving", true);
            _carStack.DriverDoor.CloseDoorFast();
            _carStack.CarMovenment.IsWork = true;
            PlayerEntered?.Invoke();
            CarPowerOn?.Invoke();
        }
    }

    private void Escape()
    {
        if (_canSeat == false)
        {
            _canSeat = true;
            _player.PlayerMovenment.IsFreezeMoving = false;
            _player.Collider.enabled = true;
            
            _player.Rigidbody.isKinematic = false;
            _player.Rigidbody.detectCollisions = true;
            _player.Transform.SetParent(default);
            _player.Animator.SetBool("isDriving", false);
            _player.StandatdCamera.SetActive(true);
            _player.CarCamera.SetActive(false);
            _carStack.CarMovenment.IsWork = false;
            _player.Transform.position = _exitPoint.position;
            _player.Transform.localEulerAngles = _exitPoint.localEulerAngles;
            PlayerQuit?.Invoke();
        }
    }
}
