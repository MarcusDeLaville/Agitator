using System;
using System.Collections.Generic;
using UnityEngine;

public enum RepositionZones
{
    Start,
    Police
}

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerStack _playerPrefab;
    [SerializeField] private CarStack _carPrefab;

    [SerializeField] private Transform _spawnPointPlayer;
    [SerializeField] private Transform _spawnPointCar;

    [SerializeField] private Transform _policePointPlayer;
    [SerializeField] private Transform _policePointCar;

    private GameObject _playerSpawned;
    private GameObject _carSpawned;
    
    private Transform _playerSpawnedTransform;
    private Transform _carSpawnedTransform; 
    
    public event Action<PlayerStack> PlayerSpawned;
    public event Action<Transform> CarSpawned;
    
    private void Start()
    {
        SpawnPlayer(_spawnPointPlayer, _spawnPointCar);
    }

    private void SpawnPlayer(Transform playerSpawnPosition, Transform carSpawnPosition)
    {
        if (_playerSpawned == true)
        {
            Destroy(_playerSpawned);
            Destroy(_carSpawned);
        }
        
        PlayerStack playerSpawned = Instantiate(_playerPrefab, playerSpawnPosition.position, playerSpawnPosition.rotation);
        CarStack carStack = Instantiate(_carPrefab, carSpawnPosition.position, carSpawnPosition.rotation);

        _playerSpawnedTransform = playerSpawned.Transform;
        _carSpawnedTransform = carStack.transform;
        _playerSpawned = _playerSpawnedTransform.gameObject;
        _carSpawned = _carSpawnedTransform.gameObject;
        
        PlayerSpawned?.Invoke(playerSpawned);
        CarSpawned?.Invoke(_carSpawnedTransform);
    }

    public void Reposition(RepositionZones repositionZones)
    {
        if (repositionZones == RepositionZones.Start)
        {
            _playerSpawnedTransform.position = _spawnPointPlayer.position;
            _carSpawnedTransform.position = _spawnPointCar.position;
        }
        else if (repositionZones == RepositionZones.Police)
        {
            SpawnPlayer(_spawnPointPlayer, _spawnPointCar);
            _playerSpawnedTransform.position = _policePointPlayer.position;
            _playerSpawnedTransform.GetComponent<PlayerStack>().PropagandaMaterialsStorage.ResettingAll();
            _carSpawnedTransform.position = _policePointCar.position;
        }
    }
}
