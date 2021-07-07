using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorCar : Door
{
    [SerializeField] private Transform _enterPoint;

    [SerializeField] private float _timeAnimation; //TODO Rework to auto cheking animationTime or Car layer
    private List<AnimatorClipInfo> _clips = new List<AnimatorClipInfo>();
    
    // public event Action<PlayerStack> OnDoorOpened;

    [SerializeField] private bool _inPlace = false;
    private float _distance;
    
    protected override void OnInteract(PlayerStack player)
    {
        player.NavMeshAgent.enabled = true;
        player.NavMeshAgent.destination = Vector3.zero;
        player.NavMeshAgent.SetDestination(_enterPoint.position);
        StartCoroutine(WalkToDoor(player));
    }

    public void CloseDoorFast()
    {
        DoorTransform.localEulerAngles = Vector3.zero;
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private IEnumerator WalkToDoor(PlayerStack player)
    {
        PlayerStack templatePlayer = player;
        
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _distance = Vector3.Distance(templatePlayer.Transform.position, _enterPoint.position);

            if (_distance <= 1.5f)
            {
                break;
            }
        }

        templatePlayer.NavMeshAgent.isStopped = false;
        templatePlayer.NavMeshAgent.destination = Vector3.zero;
        templatePlayer.NavMeshAgent.enabled = false;
        
        
        // base.OnInteract(null);
        StartCoroutine(OpenDoor(templatePlayer));
    }

    private IEnumerator OpenDoor(PlayerStack playerStack)
    {
        PlayerStack templatePlayer = playerStack;
        int layer = templatePlayer.Animator.GetLayerIndex("Car");
        templatePlayer.Animator.Play("OpenDoor", layer);
        
        // templaytePlayer.Animator.GetCurrentAnimatorClipInfo(layer, _clips);
        // float time = 0;
        // while (time == 0)
        // {
        //
        // // foreach (var item in _clips)
        // {
        //     if (item.clip.name == "OpenDoor")
        //     {
        //         time = item.clip.length;
        //     }
        // }
        // yield return new WaitUntil(() => time != 0);
        // }
        
        yield return new WaitForSeconds(_timeAnimation);
        base.OnInteract(templatePlayer);
    }
}
