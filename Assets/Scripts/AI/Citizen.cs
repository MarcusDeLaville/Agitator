using System;
using UnityEngine;
using UnityEngine.AI;

public class Citizen : Character
{
    [SerializeField] private State LookingState;
    [SerializeField] private State ReactionState;
    
    // [SerializeField] private FractionMember _fractionMember;
    
    private bool _agitatedEnd;

    public override void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        base.Awake();
    }

    public override void Update()
    {
        if (CurrentState.IsFinished == true)
        {
            SetState(StartState);
        }

        if(CurrentState.IsFinished == false)
        {
            CurrentState.Run();
        }

        if(_agitatedEnd == false)
        {
            if(FractionMember.IsAgitated)
            {
                SetState(ReactionState);
                _agitatedEnd = true;
            }
            else if(FractionMember.OnAgitation)
            {
                SetState(LookingState);
            }
        }
    }
}
