using UnityEngine;

[CreateAssetMenu]
public class RunTo : State
{
    public override void Init()
    {
        character.Agent.speed = 4;
        character.Animator.SetBool("isRun", true);
    }

    public override void Run()
    {
        character.Agent.SetDestination(character.Agent.destination);
    }
}
