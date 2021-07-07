using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SearchViolations : MonoBehaviour
{
    [SerializeField] private LayerMask _searchLayer;

    public event Action<GameObject> PassiveReactionEvent;
    public event Action ActiveReactionEvent;

    private Collider _zone;
    
    private void Start()
    {
        _zone = GetComponent<Collider>();
        StartCoroutine(UpdateSearchZone());
        // PassiveReactionEvent.AddListener(OnSearchedPassiveMaterial);
        // ActiveReactionEvent.AddListener(OnSearchedActiveMaterial);
    }

    private IEnumerator UpdateSearchZone()
    {
        while (true)
        {
            _zone.enabled = true;
            yield return new WaitForSeconds(2f);
            _zone.enabled = false;
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (((1 << collider.gameObject.layer) & _searchLayer) != 0)
        {
            // Debug.Log("detect");
            if (collider.gameObject.TryGetComponent(out CarMegafon carMegafon))
            {
                if (carMegafon.IsActivate == true)
                {
                    ActiveReactionEvent.Invoke();
                }
                    
                // Debug.Log("active");
            }
            
            if(collider.gameObject.TryGetComponent(out AgitationCast agitationMaterial))
            {
                // Debug.Log("passive");
                PassiveReactionEvent.Invoke(agitationMaterial.gameObject);
            }
        }
    }
}
