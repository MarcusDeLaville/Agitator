using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private PlayerStack _player;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;

    [SerializeField] private float _rayLength;
    [SerializeField] private Vector3 _rayOffsetPosition;

    private void Update()
    {
        FingInteractItem();
    }

    private void FingInteractItem()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;

        if (Physics.Raycast(transform.position + _rayOffsetPosition, transform.TransformDirection(Vector3.forward), out hit, _rayLength, layerMask))
        {
            Debug.DrawRay(transform.position + _rayOffsetPosition, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.TryGetComponent(out InteractItem interactItem))
            {
                // print(interactItem.name);
                
                if (Input.GetKeyDown(_interactKey))
                {
                    interactItem.InteractCollisionEvent?.Invoke(_player);
                    interactItem.interactEvent?.Invoke(_player);
                }
            }
            else
            {
                interactItem = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InteractItem interactebleItem))
        {

            if (interactebleItem.IsCollisionInteraction)
            {
                interactebleItem.InteractCollisionEvent?.Invoke(_player);
            }
        }
    }
}