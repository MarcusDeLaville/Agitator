using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoiceReactionCitizen : MonoBehaviour
{
    [SerializeField] private AudioSource _voiceSourse;
    [SerializeField] private AudioClip[] _agitationReaction;
    [SerializeField] private AudioClip[] _liveReaction;
    
    [SerializeField] private FractionMember _fractionMember;

    private void Awake()
    {
        // _fractionMember = GetComponentInParent<FractionMember>();
        // _fractionMember.OnStartedAgitation += Negative;
        // _fractionMember.OnEndedAgitation += Active;
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        _voiceSourse.PlayOneShot(_liveReaction[Random.Range(0, _liveReaction.Length)]);
    }
    // [UnityEngine.ContextMenu("Negative")]
    // private void Negative()
    // {
            // _voiceSourse.PlayOneShot(_negativeReaction[Random.Range(0, _negativeReaction.Length)]);
    // }

    [UnityEngine.ContextMenu("Active")]
    private void Active()
    {
        _voiceSourse.PlayOneShot(_agitationReaction[Random.Range(0, _agitationReaction.Length)]);
    }
    
}
