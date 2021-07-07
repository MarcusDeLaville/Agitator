using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRegistrationPlate : MonoBehaviour
{
    [SerializeField] private List<RegistarationPlate> _registarationPlatesPrefabs;
    [SerializeField] private Renderer[] _registarationPlateRenderers;

    private void Start()
    {
        PickRandomPlate();
    }

    private void PickRandomPlate()
    {
        RegistarationPlate registarationPlate;
        registarationPlate = _registarationPlatesPrefabs[Random.Range(0, _registarationPlatesPrefabs.Count)];

        for (int i = 0; i < _registarationPlateRenderers.Length; i++)
        {
            _registarationPlateRenderers[i].material.mainTexture = registarationPlate._albedo;
            _registarationPlateRenderers[i].material.SetTexture("_BumpMap", registarationPlate._normal);
        }
        
        
    }
}
