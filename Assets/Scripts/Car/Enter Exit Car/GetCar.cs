using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class GetCar : MonoBehaviour
{
    [SerializeField] private Door _driverDoor;
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private CarStack _carStack;
    [SerializeField] private Transform _playerContainer;

    [SerializeField] private float _timeAnimation;
    
    private bool _canSeat = true;
    private PlayerStack _player;
    
    private void Start()
    {
        _driverDoor.OnDoorOpened += GetDown;
    }

    private void GetDown(PlayerStack playerStack)
    {
        _player = playerStack;
        _player.NavMeshAgent.enabled = true;
        // _player.NavMeshAgent.destination = Vector3.zero;
        _player.NavMeshAgent.SetDestination(_entryPoint.position);
        StartCoroutine(MoveToEnter());
    }

    private IEnumerator MoveToEnter()
    {
        bool hui = false;
        
        while (hui == false)
        {
            yield return new WaitForSeconds(0.1f);
            var _distance = Vector3.Distance(_player.Transform.position, _entryPoint.position);
        
            if (_distance <= 1.5f)
            {
                hui = true;
            }
        }

        _player.NavMeshAgent.Stop();
        // _player.NavMeshAgent.destination = Vector3.zero;
        _player.NavMeshAgent.enabled = false;
        _carStack.InteractionCar.Enter(_player);
    }

   
}
