using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrawAgitationPoints : MonoBehaviour
{
   [SerializeField] private FractionMember _fractionMember;
   [SerializeField] private Text _pointsText;

   private void Start()
   {
      StartCoroutine(DrawPoints());
   }
   
   private IEnumerator DrawPoints()
   {
      while (true)
      {
         if (_fractionMember.IsAgitated == false)
         {
            _pointsText.text = $"{_fractionMember.AgitationPoints}/100";
         }
         else
         {
            _pointsText.text = "";
            break;
         }
         yield return new WaitForSeconds(0.1f);
      }
   }
   
}
