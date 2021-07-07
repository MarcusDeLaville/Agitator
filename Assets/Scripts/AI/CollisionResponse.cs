using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CollisionResponse : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Ragdoll _ragdoll;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private float _forse = 2f;

    [SerializeField] private float _recoveryDelay = 2f;
    
    public void AddPush(Vector3 direction)
    {
        _navMeshAgent.isStopped = true;
        _ragdoll.ActivateRagdoll();
        direction.y = 1;
        _rigidbody.AddForce(direction * _forse, ForceMode.VelocityChange);

        StartCoroutine(Recovery());
    }

    private IEnumerator Recovery()
    {
        yield return new WaitForSeconds(_recoveryDelay);
        _navMeshAgent.isStopped = false;
        _ragdoll.DeactivateRagdoll();
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        var layerIndex = _animator.GetLayerIndex("Base Layer");
        _animator.Play("Kip Up", layerIndex);
        _animator.Update(0);
    }
}
