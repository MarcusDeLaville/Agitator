using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Fractions
{
    None = 0,
    Patriot = 1,
    Pioneer = 2,
    Religion = 3,
    Riot = 4
}

public class FractionMember : MonoBehaviour
{
    [SerializeField] private Fractions _fraction;

    [SerializeField] private bool _onAgitation = false;
    [SerializeField] private bool _isAgitated = false;

    [SerializeField] private float _agitationPoints;

    [SerializeField] private GameObject _ribbonModel;
    private Coroutine _agitation;
    private Coroutine _discardAgitation;

    public float AgitationPoints => _agitationPoints;
    
    public bool OnAgitation => _onAgitation;
    public bool IsAgitated => _isAgitated;

    public Fractions MemberFraction => _fraction;

    public event Action OnStartedAgitation;
    public event Action OnEndedAgitation;

    
    private void Update()
    {
        CheakOnAgitation();
    }

    private void Start()
    {
        CitizenFractionCounter.instance.AddParticipants(this);
    }

    private void OnEnable()
    {
        // CitizenFractionCounter.instance.AddParticipants(this);
    }

    private void OnDisable()
    {
        // CitizenFractionCounter.instance.DepriveParticipants(this);
    }

    private void OnDestroy()
    {
        // CitizenFractionCounter.instance.DepriveParticipants(this);
    }

    public void DoAgitation(float agitationPointsPerTick)
    {
        OnStartedAgitation?.Invoke();
        if (_isAgitated == false)
        {
            if (_discardAgitation != null)
            {
                StopCoroutine(_discardAgitation);
            }
            
            _agitation = StartCoroutine(Agitation(agitationPointsPerTick));
        }
    }

    public void CanselAgitation()
    {
        StopCoroutine(_agitation);
        
        if (_isAgitated == false)
        {
            _discardAgitation = StartCoroutine(DepriveAgitation());
        }
    }

    private void CheakOnAgitation()
    {
        if (_agitationPoints >= 50)
        {
            _onAgitation = true;
        }
        else
        {
            _onAgitation = false;
        }
    }

    private IEnumerator Agitation(float points)
    {
        while (_isAgitated == false)
        {
            yield return new WaitForSeconds(0.7f);
            _agitationPoints += points;

            if (_agitationPoints >= 100)
            {
                _isAgitated = true;
                CitizenFractionCounter.instance.DepriveParticipants(this);
                SetFraction(Fractions.Pioneer);
                CitizenFractionCounter.instance.AddParticipants(this);
                _ribbonModel.SetActive(true);
                OnEndedAgitation?.Invoke();
            }
            else if(_agitationPoints >= 50)
            {
                _onAgitation = true;
            }
        }
    }

    private IEnumerator DepriveAgitation()
    {
        while (_agitationPoints > 0)
        {
            yield return new WaitForSeconds(0.7f);
            _agitationPoints -= 7;
        }

        _agitationPoints = 0;
    }

    public void SetFraction(Fractions fractions)
    {
        _fraction = fractions;

        if(_fraction == Fractions.Pioneer)
        {
            _ribbonModel.SetActive(true);
        }
        else
        {
            _ribbonModel.SetActive(false);
        }
    }
    
    private void PickRandomFraction()
    {
        int seed = Random.Range(0, 2);

        switch (seed)
        {
            case 0:
                _fraction = Fractions.Patriot;
                break;
            case 1:
                _fraction = Fractions.Religion;
                break;
            case 2:
                _fraction = Fractions.Riot;
                break;
            default:
                _fraction = Fractions.Riot;
                break;
        }
    }
}
