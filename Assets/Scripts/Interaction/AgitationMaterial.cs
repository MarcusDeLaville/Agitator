using System;
using UnityEngine;

public class AgitationMaterial : InteractItem
{
    [SerializeField] private PropagandaMaterial _propagandaMaterial;
    [SerializeField] private MeshRenderer _materialRenderer;
    private bool _isCollected = false;

    private void Awake()
    {
        _materialRenderer = GetComponent<MeshRenderer>();
    }

    protected override void OnCollisionInteraction(PlayerStack playerStack)
    {
        if (_isCollected == false)
        {
            playerStack.PropagandaMaterialsStorage.AddMaterials(_propagandaMaterial);
            _materialRenderer.enabled = false;
            Destroy(gameObject, 1.5f);
            _isCollected = true;
        }
        
    }
}
