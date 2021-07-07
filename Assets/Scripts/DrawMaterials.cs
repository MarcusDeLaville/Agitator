using System;
using UnityEngine;
using UnityEngine.UI;

public class DrawMaterials : MonoBehaviour
{
    [SerializeField] private PropagandaMaterialsStorage _propagandaMaterialsStorage;
    [SerializeField] private PlayerSpawner _playerSpawner;
    
    [SerializeField] private Text _leafletsSmall;
    [SerializeField] private Text _leafletsBig;
    [SerializeField] private Text _paperSmall;
    [SerializeField] private Text _paperBig;
    [SerializeField] private Text _poster;

    private void Awake()
    {
        _playerSpawner.PlayerSpawned += SetStorage;
    }

    private void SetStorage(PlayerStack playerStack)
    {
        _propagandaMaterialsStorage = playerStack.PropagandaMaterialsStorage;
        _propagandaMaterialsStorage.ChangetMaterialsCount += Draw;
        Draw();
    }
    
    private void Draw()
    {
        _leafletsSmall.text = _propagandaMaterialsStorage.LeafletsSmall.ToString();
        _leafletsBig.text = _propagandaMaterialsStorage.LeafletsBig.ToString();
        _paperSmall.text = _propagandaMaterialsStorage.PaperSmall.ToString();
        _paperBig.text = _propagandaMaterialsStorage.PaperBig.ToString();
        _poster.text = _propagandaMaterialsStorage.Poster.ToString();
    }
}
