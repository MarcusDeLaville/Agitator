using UnityEngine;

[CreateAssetMenu]
public class Chase : State
{
    public int ArrestDistance;
    
    public override void Init()
    {
        character.Agent.speed = 6;
        character.Animator.SetBool("isRun", true);
    }

    public override void Run()
    {
        character.Agent.SetDestination(character.PlayerTransform.position);

        
        //todo: дубляж
        if (Vector3.Distance(character.transform.position, character.PlayerTransform.position) < ArrestDistance)
        {
            character.Agent.speed = 2;
            character.Animator.SetBool("isRun", false);
            IsFinished = true;
        }
        else if (Vector3.Distance(character.transform.position, character.PlayerTransform.position) > 30)
        {
            character.Agent.speed = 2;
            character.Animator.SetBool("isRun", false);
            IsFinished = true;
        }
    }
}
