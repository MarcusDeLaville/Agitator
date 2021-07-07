using UnityEngine;

public enum Walk
{
    ForwardOnWay = 0,
    BackOnWay = 1
}

[CreateAssetMenu]
public class WalkState : State
{
    private Vector3 _targetPosition;
    private int _indexCurrentPoint;

    private Walk _walkDirection;
    
    public override void Init()
    {
        character.Agent.speed = 2;
        character.Animator.SetBool("isRun", false);
        
        character.Agent.isStopped = false;
        _targetPosition = character.CurrentPathPoins.Points[0].position;
        
        for (int i = 0; i < character.CurrentPathPoins.Points.Count; i++)
        {
            float distanceCharacterToTarget = Vector3.Distance(_targetPosition, character.transform.position);
            float distanceCharacterToPoint = Vector3.Distance(character.CurrentPathPoins.Points[i].position,character.transform.position);
            
            if (distanceCharacterToPoint < distanceCharacterToTarget)
            {
                _targetPosition = character.CurrentPathPoins.Points[i].position;
                _indexCurrentPoint = i;
            }
        }
    }

    public override void Run()
    {
        character.Animator.SetBool("isWalking", true);

        if (Vector3.Distance(_targetPosition, character.transform.position) <= 2f)
        {
            MoveNext();
        }
        
        character.MoveTo(_targetPosition);
    }

    private void MoveNext()
    {
        if (_walkDirection == Walk.ForwardOnWay)
        {
            _indexCurrentPoint++;
        }
        else
        {
            _indexCurrentPoint--;
        }

        if(_indexCurrentPoint > character.CurrentPathPoins.Points.Count - 1)
        {
            _indexCurrentPoint = character.CurrentPathPoins.Points.Count - 1;
            _walkDirection = Walk.BackOnWay;
        }
        
        if(_indexCurrentPoint < 0)
        {
            _indexCurrentPoint = 0;
            _walkDirection = Walk.ForwardOnWay;
        }
        
        _targetPosition = character.CurrentPathPoins.Points[_indexCurrentPoint].position;
    }
}
