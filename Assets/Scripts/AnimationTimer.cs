using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationTimer : MonoBehaviour
{
    private readonly List<AnimatorClipInfo> _clips = new List<AnimatorClipInfo>();
    private Animator _animator;

    protected abstract string AnimatorLayer { get; }

    protected abstract IEnumerator WaitAnimationEnding(float waitingTime);

    protected void Init(Animator animator)
    {
        _animator = animator;
    }

    protected int TryGetAnimatorLayerIndex(string AnimatorLayer)
    {
        var layerIndex = _animator.GetLayerIndex(AnimatorLayer);

        if (layerIndex == -1)
            Debug.LogError("Animator Layer отсутствует или неправильно указан. Соотнесите имена");

        return layerIndex;
    }

    protected void EnableAnimation(string animationName, int layerIndex)
    {
        _animator.Play(animationName, layerIndex);
        _animator.Update(0);
    }

    protected float FindCalledAnimationClipTime(int layerIndex, string animationName)
    {
        _animator.GetCurrentAnimatorClipInfo(layerIndex, _clips);

        float time = 0;
        foreach (var item in _clips)
        {
            if (item.clip.name == animationName)
            {
                time = item.clip.length;
            }
        }
        return time;
    }
}