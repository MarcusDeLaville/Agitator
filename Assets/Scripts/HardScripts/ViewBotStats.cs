using UnityEngine;

public class ViewBotStats : MonoBehaviour
{
    [SerializeField] private CanvasGroup _statsCanvasGroup;
    [SerializeField] private LayerMask _playerLayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _playerLayer) != 0)
        {
            _statsCanvasGroup.alpha = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & _playerLayer) != 0)
        {
            _statsCanvasGroup.alpha = 0;
        }
    }
}
