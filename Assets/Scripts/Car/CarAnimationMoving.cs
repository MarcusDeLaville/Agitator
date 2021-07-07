using System.Collections;
using UnityEngine;
using DG.Tweening;

public abstract class CarAnimationMoving : AnimationTimer
{
    protected PlayerStack Player { get; private set; }

    protected override string AnimatorLayer => "Car";
    protected abstract string AnimationName { get; }

    protected abstract void OnEnable();
    protected abstract void OnDisable();
    protected abstract Vector3 GetTargetPosition();
    protected abstract void CallActionsAfterYield();

    protected override IEnumerator WaitAnimationEnding(float waitingTime)
    {
        var targetPosition = GetTargetPosition();
        MovePlayerToTargetPosition(targetPosition, waitingTime);
        yield return new WaitForSeconds(waitingTime);
        CallActionsAfterYield();
    }

    protected void MovePlayerToTargetPosition(Vector3 targetPosition, float time)
    {
        Player.gameObject.transform.DOMove(targetPosition, time);
    }

    protected void OnStartingAnimation(PlayerStack player)
    {
        Player = player;

        //Player.PlayerMovenment.IAmBusy = true;

        Init(Player.Animator);
        var layerIndex = TryGetAnimatorLayerIndex(AnimatorLayer);
        EnableAnimation(AnimationName, layerIndex);
        var time = FindCalledAnimationClipTime(layerIndex, AnimationName);
        StartCoroutine(WaitAnimationEnding(time));
    }
}