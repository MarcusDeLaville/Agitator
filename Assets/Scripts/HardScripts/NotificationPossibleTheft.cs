using System;
using System.Collections;
using UnityEngine;

public class NotificationPossibleTheft : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Transform _carTransform;

    [SerializeField] private ContextPanelAnimation _notificationPanel;
    [SerializeField] private PlayerSpawner _playerSpawner;
    
    [SerializeField] private float _diseredNotifivationDistance = 40;
    
    private void Start()
    {
        // _playerPosition = GameObject.FindObjectOfType<PlayerMovenment>().GetComponent<Transform>();
        // _carTransform = GameObject.FindObjectOfType<CarMegafon>().GetComponent<Transform>();

        _playerSpawner.PlayerSpawned += SetPlayerPosition;
        _playerSpawner.CarSpawned += SetCarPosition;
        
        // StartCoroutine(CheakDistenceNotification());
    }

    private void SetPlayerPosition(PlayerStack player)
    {
        _playerPosition = player.Transform;
    }

    private void SetCarPosition(Transform car)
    {
        _carTransform = car;
        StartCoroutine(CheakDistenceNotification());
    }
    
    private IEnumerator CheakDistenceNotification()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (Vector3.Distance(_playerPosition.position, _carTransform.position) > _diseredNotifivationDistance)
            {
                _notificationPanel.ShowPanel();
                yield return new WaitForSeconds(1000f);
            }
        }
    }
}
