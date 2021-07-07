using UnityEngine;

[CreateAssetMenu]
public class ReactionState : State
{
    private Vector3 _targetPosition;
    private int _indexCurrentPoint;
    private float _elapsedTime;
    private float _gateSecondsTime;
    public float AnimationTime;
    
    public override void Init()
    {
        character.Agent.Stop();
        character.Animator.SetBool("isAgitate", true);
    }

    public override void Run()
    {
         _gateSecondsTime += Time.deltaTime;

        if (_gateSecondsTime >= 1)
        {
            _elapsedTime++;
            _gateSecondsTime = 0;
        }

        if (_elapsedTime >= AnimationTime)
        {
            character.Animator.SetBool("isAgitate", false);
            IsFinished = true;
        }
    }
}
