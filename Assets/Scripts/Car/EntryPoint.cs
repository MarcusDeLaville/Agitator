using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private CarInteraction _carInteraction;

    public event UnityAction<PlayerStack> Reached;

    public Transform EnterPoint => _entryPoint;
    public float YPos { get; private set; }

    private void OnEnable()
    {
        _carInteraction.PlayerSeating += OnPlayerSeating;
    }

    private void OnDisable()
    {
        _carInteraction.PlayerSeating -= OnPlayerSeating;
    }

    private void OnPlayerSeating(PlayerStack player)
    {
        YPos = player.transform.position.y;
        StartCoroutine(MoveToEntryPoint(player, YPos));
    }

    private IEnumerator MoveToEntryPoint(PlayerStack player, float yPosition)
    {
        var entryPoint = new Vector3(_entryPoint.position.x, yPosition, _entryPoint.position.z);
        while (player.transform.position != entryPoint)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, entryPoint, 0.5f * Time.deltaTime);
        }

        Reached?.Invoke(player);
        yield return null;
    }
}