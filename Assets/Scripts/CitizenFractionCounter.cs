using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Fraction
{
    public string Name => Fractions.ToString();
    public Fractions Fractions;
    public int MaximumOnDistrict;
    public List<FractionMember> ParticipantsFraction;
    public Text TextCount; //TODO Вывести отрисовку значений в отдельный скрипт
}

public class CitizenFractionCounter : MonoBehaviour
{
    [SerializeField] private List<Fraction> _fractions;
    [SerializeField] private List<FractionMember> _allParticipants;

    public static CitizenFractionCounter instance = null;
    public List<Fraction> Fractions => _fractions;
    public List<FractionMember> AllParticipants => _allParticipants;

    public event Action ParticipantAdded;
    
    //TODO ХАРД СКРИПТ НАДО БУДЕТ ПЕРЕПИСАТЬ
    private void Start()
    {
        if (instance == null) 
        {
            instance = this;
        }
        //oтказаться от синглтона
    }

    public void AddParticipants(FractionMember fractionMember)
    {
        for (int i = 0; i < _fractions.Count; i++)
        {
            if (fractionMember.MemberFraction == _fractions[i].Fractions)
            {
                _fractions[i].ParticipantsFraction.Add(fractionMember);
                _fractions[i].TextCount.text = _fractions[i].ParticipantsFraction.Count.ToString();
            }
        }

        CalculateAllParticipants();
        ParticipantAdded?.Invoke();
    }

    public void DepriveParticipants(FractionMember fractionMember)
    {
        foreach (var fraction in _fractions)
        {
            if (fractionMember.MemberFraction == fraction.Fractions)
            {
                fraction.ParticipantsFraction.Remove(fractionMember);
                fraction.TextCount.text = fraction.ParticipantsFraction.Count.ToString();
            }
        }

        CalculateAllParticipants();
    }

    private void CalculateAllParticipants()
    {
        _allParticipants.Clear();
        
        foreach (var fraction in _fractions)
        {
            _allParticipants.AddRange(fraction.ParticipantsFraction);
        }
    }
    
}
