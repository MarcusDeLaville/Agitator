using System;
using UnityEngine;

public class CarBumper : MonoBehaviour
{
    [SerializeField] private float _requiredSpeed;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out CollisionResponse collisionResponse))
        {
            print("bump");
            collisionResponse.AddPush(gameObject.transform.TransformDirection(Vector3.forward));
        }
    }
}
