using UnityEngine;
using Random = UnityEngine.Random;

public class BotVoice : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _phrases;
    
    private void OnTriggerEnter(Collider other)
    {
        _audioSource.PlayOneShot(_phrases[Random.Range(0, _phrases.Length)]);
    }
}
