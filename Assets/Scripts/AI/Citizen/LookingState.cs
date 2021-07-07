using UnityEngine;

[CreateAssetMenu]
public class LookingState : State
{
    public override void Init()
    {
        character.Animator.SetBool("isWalking", false);
        character.Agent.Stop();
    }

    public override void Run()
    {
        character.transform.LookAt(character.PlayerTransform.position + character.PlayerTransform.rotation * Vector3.back,character.transform.rotation * Vector3.up);
    }
}
