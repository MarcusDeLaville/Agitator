using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCloseControl : MonoBehaviour
{
    [SerializeField] private List<ContextPanelAnimation> _openedPanels;

    public int OpenedPanelsCount => _openedPanels.Count;
    
    public void AddPanel(ContextPanelAnimation contextPanelAnimation)
    {
        _openedPanels.Add(contextPanelAnimation);
    }

    public void DeprivePanel(ContextPanelAnimation contextPanelAnimation)
    {
        // _openedPanels.Remove(contextPanelAnimation);
        StartCoroutine(RemoveDelay(contextPanelAnimation));
    }

    private IEnumerator RemoveDelay(ContextPanelAnimation contextPanelAnimation)
    {
        yield return new WaitForSeconds(2f);
        _openedPanels.Remove(contextPanelAnimation);
    }
}
