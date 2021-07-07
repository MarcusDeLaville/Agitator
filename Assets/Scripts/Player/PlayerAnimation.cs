using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovenment _playerMovenment;
    [SerializeField] private Animator _animator;
    [SerializeField] private GroundDetection _groundDetection;

    private bool _isTurn;

    private void Update()
    {
        Vector3 moveDirection = transform.InverseTransformDirection(_playerMovenment.MoveDirection);
        float mouseX = Input.GetAxis("Mouse X");

        float verticalAmount = moveDirection.z;
        float horizontalAmount = moveDirection.x;

        //_isTurn = _isTurn ? mouseX != 0 : mouseX == 0;

        if (_playerMovenment.IsRun == false)
        {
            verticalAmount /= 2;
        }

        float runCycle = Mathf.Repeat(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.2f, 1.0f);
        float jumpLeg = (runCycle < 0.5f ? 1.0f : -1.0f) * verticalAmount;

        _animator.SetFloat("Forward", verticalAmount, 0.1f, Time.deltaTime);
        _animator.SetFloat("Horizontal", horizontalAmount, 0.1f, Time.deltaTime);
        _animator.SetBool("OnGround", _groundDetection.IsGrounded);     
        //_animator.SetBool("Turn", _isTurn);

        if (_groundDetection.IsGrounded)
        {
            _animator.SetFloat("Jump", _playerMovenment.PlayerVelocity.y, 0.1f, Time.deltaTime);
        }


        if (_groundDetection.IsGrounded)
        {
            _animator.SetFloat("JumpLeg", jumpLeg);
        }       
    }
}
