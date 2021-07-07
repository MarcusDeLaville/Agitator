using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _rigidbodies;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
        _rigidbodies.RemoveAt(0);

        DeactivateRagdoll();
    }

    [UnityEngine.ContextMenu("Active")]
    public void ActivateRagdoll()
    {
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;
        }

        _animator.enabled = false;
    }

    [UnityEngine.ContextMenu("Deactive")]
    public void DeactivateRagdoll()
    {
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        _animator.enabled = true;
    }
}