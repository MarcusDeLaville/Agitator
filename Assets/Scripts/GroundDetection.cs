using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] private Transform _cheakingPoint;
    [SerializeField] private float _rayLenght;
    private bool _isGrounded;
    public bool IsGrounded => _isGrounded;

    private void FixedUpdate()
    {
        CheakGround();
    }

    private void CheakGround()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;


        if(Physics.Raycast(_cheakingPoint.position, Vector3.down, out hit, _rayLenght))
        {
            Debug.DrawRay(_cheakingPoint.position,Vector3.down * hit.distance, Color.cyan);
        }

        if (hit.collider != null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
}
