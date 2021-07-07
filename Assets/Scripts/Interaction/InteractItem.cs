using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InteractItem : MonoBehaviour
{
    public UnityEvent<PlayerStack> interactEvent;
    public UnityEvent<PlayerStack> InteractCollisionEvent;
    
    public bool IsCollisionInteraction = false;

    protected virtual void OnEnable()
    {
        if (IsCollisionInteraction)
        {
            InteractCollisionEvent.AddListener(OnCollisionInteraction);
        }
        else
        {
            interactEvent.AddListener(OnInteract);
        }
    }

    protected virtual void OnDisable()
    {
        if (IsCollisionInteraction)
        {
            InteractCollisionEvent.RemoveListener(OnCollisionInteraction);
        }
        else
        {
            interactEvent.RemoveListener(OnInteract);
        }
    }

    protected virtual void OnCollisionInteraction(PlayerStack playerStack = null) { }

    protected virtual void OnInteract(PlayerStack player = null) { }
}