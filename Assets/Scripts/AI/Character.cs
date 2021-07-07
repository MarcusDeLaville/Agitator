using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Animator Animator;
    public Transform PlayerTransform;
    public FractionMember FractionMember;
    
    public State StartState;
    public State CurrentState;
    
    public PathPoints CurrentPathPoins;

    public Vector3 TargetPosition;

    private PlayerSpawner _playerSpawner;
    private bool _isCurrentStateNull;

    public virtual void Awake()
    {
        FractionMember = GetComponent<FractionMember>();
        _playerSpawner = FindObjectOfType<PlayerSpawner>();
        _playerSpawner.PlayerSpawned += SetPlayerTransform;
        
        // SetState(StartState);
    }

    private void Start()
    {
        if (CurrentState == null)
        {
            SetState(StartState);
        }
    }

    public virtual void Update()
    {
        if (CurrentState.IsFinished == true)
        {
            SetState(StartState);
        }
    }

    public void SetPathPoints(PathPoints way)
    {
        CurrentPathPoins = way;
    }
    
    private void SetPlayerTransform(PlayerStack playerStack)
    {
        PlayerTransform = playerStack.Transform;
    }
    
    public void SetState(State state)
    {
        CurrentState = Instantiate(state);
        CurrentState.character = this;
        CurrentState.Init();
    }

    public void MoveTo(Vector3 position)
    {
        TargetPosition = position;
        Agent.SetDestination(TargetPosition);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(Vector3.up + TargetPosition, 0.5f);
    // }
}
