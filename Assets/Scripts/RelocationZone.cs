using UnityEngine;

public class RelocationZone : MonoBehaviour
{
    [SerializeField] private ContextPanelAnimation _relocationPanel;
    [SerializeField] private LayerMask _playerLayer;
    private bool _isEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _playerLayer) != 0)
        {
            if (_isEntered == false)
            {
                _isEntered = true;
                _relocationPanel.ShowPanel();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        _isEntered = false;
    }
}
