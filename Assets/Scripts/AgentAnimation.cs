using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentAnimation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        StartCoroutine(ChekingStatus());
    }

    private IEnumerator ChekingStatus()
    {
        while (true)
        {
            if (_navMeshAgent.enabled == true)
            {
                _animator.SetBool("MoveTo", true);
            
            }
            else
            {
                _animator.SetBool("MoveTo", false);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
