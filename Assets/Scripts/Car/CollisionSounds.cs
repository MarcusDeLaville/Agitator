using System;
using UnityEngine;


public class CollisionSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _collisionClips;

    [SerializeField] private Rigidbody _rigidbody;
    
    private void Start()
    {
        _audioSource.clip = _collisionClips;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("столкновение");
        
        if (!_audioSource.isPlaying)
        {
            // _audioSource.volume = Mathf.Clamp(_rigidbody.velocity.magnitude / 10.0f, 0.1f, 1.0f);
            _audioSource.volume = other.impulse.magnitude * 0.01f;
            _audioSource.Play();
        }
        
        // if (other.relativeVelocity.magnitude > 2)
        // {
        //     _audioSource.clip = _collisionClips;
        //     _audioSource.Play();
        // }
    }
}
