using UnityEngine;
using UnityEngine.Events;

public class EntryInside : CarAnimationMoving
{
    [SerializeField] private Transform _targetPlace;
    [SerializeField] private CarDoor _door;
    [SerializeField] private EntryPoint _entryPoint;

    public event UnityAction<EntryInside> PlayerSeated;

    public CarDoor Door => _door;
    protected override string AnimationName => "SeatInCar";

    protected override void OnEnable()
    {
        _entryPoint.Reached += OnStartingAnimation;
    }

    protected override void OnDisable()
    {
        _entryPoint.Reached -= OnStartingAnimation;
    }

    protected override Vector3 GetTargetPosition()
    {
        return _targetPlace.position;
    }

    protected override void CallActionsAfterYield()
    {
        PlayerSeated?.Invoke(this);
    }
}