using UnityEngine;

public class PlayerMovenment : MonoBehaviour
{
    public Vector3 PlayerVelocity => _playerVelocity;
    public Vector3 MoveDirection => _moveDirection;
    public bool IsRun { get; private set; } = false;

    public bool IsFreezeMoving;

    [Header("Input")]
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;
    [SerializeField] private KeyCode _runButton = KeyCode.LeftShift;

    [Header("Move Settings")]
    [Range(1f, 10.0f)]
    [SerializeField] private float _walkSpeed = 1.5f;
    [Range(2f, 15.0f)]
    [SerializeField] private float _runSpeed = 2.5f;
    [Range(5f, 1500.0f)]
    [SerializeField] private float _jumpForse = 8f;

    [SerializeField] private float _sensetivity = 2f;
    
    [Header("Additional")]
    [SerializeField] private CameraRotation _rotatationCamera;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundDetection _groundDetection;

    private float _globalSpeed;
    private Vector3 _playerVelocity;
    private Vector3 _moveDirection;

    private void FixedUpdate()
    {
        if (IsFreezeMoving)
        {
            return;
        }

        Move();

        if (_groundDetection.IsGrounded == true && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0;
        }

        if (_groundDetection.IsGrounded == true && Input.GetKeyDown(_jumpButton))
        {
            Jump();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseX;

        if (_rotatationCamera.IsCameraRotate == true)
        {
            mouseX = 0;
        }
        else
        {
            mouseX = Input.GetAxis("Mouse X");
        }
        
        _globalSpeed = Input.GetKey(_runButton) ? _runSpeed : _walkSpeed;

        IsRun = Input.GetKey(_runButton);

        _moveDirection = new Vector3(horizontal, 0, vertical);

        _moveDirection = transform.TransformDirection(_moveDirection);
        gameObject.transform.localEulerAngles += new Vector3(0, mouseX * _sensetivity, 0);

        _rigidbody.MovePosition(transform.position + _moveDirection * _globalSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForse, ForceMode.Impulse);
    }
}

