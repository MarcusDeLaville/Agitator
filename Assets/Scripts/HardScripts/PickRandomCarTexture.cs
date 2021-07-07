using UnityEngine;
using Random = UnityEngine.Random;

public class PickRandomCarTexture : MonoBehaviour
{
    [SerializeField] private Texture[] _textures;
    private Renderer[] _renderers;

    private void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        PickRandomPlate();
    }

    private void PickRandomPlate()
    {
        Texture randomTexture = _textures[Random.Range(0, _textures.Length)];
        
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.mainTexture = randomTexture;
        }
    }
}
