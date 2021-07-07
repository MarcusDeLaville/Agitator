using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CitizenSpawner : MonoBehaviour
{
    [SerializeField] private Citizen[] _participantsPrefabs;
    [SerializeField] private CitizenFractionCounter _citizenFractionCounter;
    [SerializeField] private List<PathPoints> _citizenWays;
    [SerializeField] private List<GameObject> _citizenTemplate;

    private void Start()
    {
        SpawnCitizens();
    }

    private void SpawnCitizens()
    {
        for (int i = 0; i < _citizenFractionCounter.Fractions.Count; i++)
        {
            for (int y = 0; y < _citizenFractionCounter.Fractions[i].MaximumOnDistrict; y++)
            {
                Citizen templateCitizen;
                PathPoints randomWay = _citizenWays[Random.Range(0, _citizenWays.Count)];
                templateCitizen = Instantiate(_participantsPrefabs[Random.Range(0, _participantsPrefabs.Length)], randomWay.Points[Random.Range(0, randomWay.Points.Count)].position, Quaternion.identity);
                templateCitizen.FractionMember.SetFraction(_citizenFractionCounter.Fractions[i].Fractions);
                templateCitizen.SetPathPoints(randomWay);
            }    
        }
    }

}
