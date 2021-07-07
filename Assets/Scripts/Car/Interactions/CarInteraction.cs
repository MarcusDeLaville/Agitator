using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CarInteraction : InteractItem
{
    [SerializeField] private Transform _playerContainer;
    [SerializeField] private CarStack _carStack;
    
    private PlayerStack _player;
    private bool _canSeat = true;

    public event UnityAction<PlayerStack> PlayerSeating;
    public event UnityAction<PlayerStack> PlayerEscaping;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Escape();
        }
    }

    protected override void OnInteract(PlayerStack player)
    {
        if (_canSeat == true)
        {
            _canSeat = false;
            _player = player;
            _player.PlayerMovenment.IsFreezeMoving = true;


            _player.Collider.enabled = false;
            /*
            _player.Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            */
            _player.Rigidbody.isKinematic = true;
            _player.Rigidbody.detectCollisions = false;
            
            _player.Transform.SetParent(_playerContainer);
            _player.Transform.localEulerAngles = new Vector3(0, -180, 0);
            _player.Animator.SetBool("isDriving", true);
            _player.StandatdCamera.SetActive(false);
            _player.CarCamera.SetActive(true);
            // _player.CarCamera.GetComponent<CinemachineVirtualCamera>()
            //     .GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, m_FollowOffset, _carStack.CameraDistance);

            var cinemachineVirtualCamera = _player.CarCamera.GetComponent<CinemachineVirtualCamera>()
                .GetCinemachineComponent<CinemachineTransposer>();
            
            cinemachineVirtualCamera.m_FollowOffset = new Vector3(cinemachineVirtualCamera.m_FollowOffset.x, cinemachineVirtualCamera.m_FollowOffset.y, _carStack.CameraDistance);
        
            PlayerSeating?.Invoke(_player);
        }
    }

    private void Escape()
    {
        if (_canSeat == false)
        {
            _canSeat = true;
            _player.PlayerMovenment.IsFreezeMoving = false;
            _player.Collider.enabled = true;
            /*
            _player.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            */
            _player.Rigidbody.isKinematic = false;
            _player.Rigidbody.detectCollisions = true;
            
            _player.Transform.SetParent(default);
            _player.Transform.localEulerAngles = new Vector3(0, -180, 0);
            _player.Animator.SetBool("isDriving", false);
            _player.StandatdCamera.SetActive(true);
            _player.CarCamera.SetActive(false);

            PlayerEscaping?.Invoke(_player);
        }
    }
}