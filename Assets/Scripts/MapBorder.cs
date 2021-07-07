using System;
using UnityEngine;

public class MapBorder : MonoBehaviour
{
    [SerializeField] private ContextPanelAnimation _borderPanel;
    [SerializeField] private LayerMask _playerLayer;

    private bool _isEntered = false;

    // [SerializeField] private PlayerSpawner _playerSpawner;
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _playerLayer) != 0)
        {
            if (_isEntered == false)
            {
                // _playerSpawner.Reposition(RepositionZones.Start);
                // other.gameObject.GetComponent<PlayerStack>().PropagandaMaterialsStorage.ResettingAll();
                _isEntered = true;
                _borderPanel.ShowPanel();
            }
        }
        // if (other.TryGetComponent(out PlayerStack playerStack))
        // {
        //     playerStack.PropagandaMaterialsStorage.ResettingAll();
        //     _playerSpawner.Reposition(RepositionZones.Start);
        //     _borderPanel.ShowPanel();
        // }
    }

    private void OnTriggerExit(Collider other)
    {
        _isEntered = false;
    }
}
