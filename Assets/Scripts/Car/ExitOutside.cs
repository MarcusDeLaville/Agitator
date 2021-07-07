using UnityEngine;
using UnityEngine.Events;

public class ExitOutside : CarAnimationMoving
{
    [SerializeField] private EntryPoint _entryPoint;
    [SerializeField] private CarInteraction _carInteraction;

    public event UnityAction<PlayerStack> PlayerEscaped;

    protected override string AnimationName => "EscapeFromCar";

    protected override void OnEnable()
    {
        _carInteraction.PlayerEscaping += OnStartingAnimation;
    }

    protected override void OnDisable()
    {
        _carInteraction.PlayerEscaping -= OnStartingAnimation;
    }

    protected override Vector3 GetTargetPosition()
    {
        return new Vector3(_entryPoint.EnterPoint.position.x, _entryPoint.YPos, _entryPoint.EnterPoint.position.z);
    }

    protected override void CallActionsAfterYield()
    {
        PlayerEscaped?.Invoke(Player);
        Player.Collider.enabled = true;
        //Player.CharacterController.enabled = true;
    }
}