using System;
using System.Collections;
using UnityEngine;

public class AgitmaterialsSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GameObject[] _materials;

    private void Start()
    {
        StartCoroutine(SpawnPrefab());
    }

    private IEnumerator SpawnPrefab()
    {
        while (true)
        {
            for (int i = 0; i < _materials.Length; i++)
            {
                Instantiate(_materials[i], transform.position, Quaternion.identity, transform);
            }
            yield return new WaitForSeconds(30);
        }
    }
}
