using UnityEngine;

public class AgitationCast : MonoBehaviour
{
    [SerializeField] private float _agitationPointPerTick;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FractionMember fractionMember))
        {
            if (fractionMember.MemberFraction != Fractions.Pioneer)
            {
                fractionMember.DoAgitation(_agitationPointPerTick);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FractionMember fractionMember))
        {
            if (fractionMember.MemberFraction != Fractions.Pioneer)
            {
                fractionMember.CanselAgitation();
            }
        }
    }

    
}
