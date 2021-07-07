using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Policeman : Character
{
    [SerializeField] private State _passiveReaction;
    [SerializeField] private State _activeReaction;
    
    [SerializeField] private SearchViolations _searchViolations;
    [SerializeField] private List<GameObject> _targetMaterials = new List<GameObject>();
    [SerializeField] private bool _isChased = false;

    //todo: переписать под фабрику а ивент перести под сишарповский
    private PlayerSpawner _playerSpawner;
    public UnityEvent CaughtCriminalEvent;

    public bool IsChased => _isChased;

    public event Action ChaseStart;
    public event Action ChaseEnd;
    
    public List<GameObject> TargetMaterials()
    {
        _targetMaterials.RemoveAll(x => x == null);
        return _targetMaterials;
    }

    public override void Awake()
    {
        base.Awake();
        _playerSpawner = FindObjectOfType<PlayerSpawner>();
        
        _searchViolations.ActiveReactionEvent += OnSearchedActiveMaterial;
        _searchViolations.PassiveReactionEvent += OnSearchedPassiveMaterial;
    }
    
    public override void Update()
    {
        if (_isChased == true)
        {
            
            if (Vector3.Distance(transform.position, PlayerTransform.position) > 30)
            {
                _isChased = false;
                ChaseEnd?.Invoke();
            }
            if (Vector3.Distance(transform.position, PlayerTransform.position) < 4)
            {
                _isChased = false;
                _playerSpawner.Reposition(RepositionZones.Police);
                ChaseEnd?.Invoke();
                CaughtCriminalEvent?.Invoke();
            }
        }
        
        if (CurrentState.IsFinished == true)
        {
            SetState(StartState);
        }

        if(CurrentState.IsFinished == false)
        {
            CurrentState.Run();
        }
    }

    private IEnumerator CaughtMaterials()
    {
        while (TargetMaterials().Count != 0)
        {
            Agent.SetDestination(TargetMaterials()[0].transform.position);
            if (Vector3.Distance(Agent.transform.position, TargetMaterials()[0].transform.position) <= 1)
            {
                Destroy(TargetMaterials()[0].gameObject);
            }
            yield return new WaitForSeconds(1f);
        }

        SetState(StartState);
    }
    
    private void OnSearchedPassiveMaterial(GameObject material = null)
    {
        SetState(_passiveReaction);
        StartCoroutine(CaughtMaterials());
        _targetMaterials.Add(material);
    }

    private void OnSearchedActiveMaterial()
    {
        SetState(_activeReaction);
        _isChased = true;
        ChaseStart?.Invoke();
    }
    
    
}
