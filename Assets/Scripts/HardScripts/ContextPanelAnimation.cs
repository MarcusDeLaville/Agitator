using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ContextPanelAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _panelObject;
    [SerializeField] private CanvasGroup _panelCanvsGroup;

    [SerializeField] private bool _isShow = false;
    [SerializeField] private bool _escapeMenu = false;
    [SerializeField] private bool _closeAlready = false;

    [SerializeField] private List<ContextPanelAnimation> _contextPanels;
    [SerializeField] private List<ContextPanelAnimation> _templateContextPanels;

    [SerializeField] private PanelCloseControl _panelCloseControl;
    
    public bool IsShow => _isShow;

    public UnityEvent OnClosePanel;

    private void Start()
    {
        _panelObject.transform.localScale = Vector3.zero;
        _panelCanvsGroup.alpha = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_escapeMenu == true)
            {
                if (_isShow == false)
                {
                    if (_panelCloseControl.OpenedPanelsCount == 0)
                    {
                        ShowPanel();
                    }
                            // _templateContextPanels.Clear();
                        //
                        // foreach (var panel in _contextPanels)
                        // {
                        //     if (panel.IsShow == true)
                        //     {
                        //         _templateContextPanels.Add(panel);
                        //     }
                        // }
                        //
                        // if (_templateContextPanels.Count == 0)
                        // {
                        //     ShowPanel();
                        // }
                        // else
                        // {
                        //     _templateContextPanels.Clear();
                        // }
                }
                else
                {
                    HidePanel();
                }
                
            }

            if (_closeAlready == true)
            {
                if (_isShow == true)
                {
                    HidePanel();
                }
            }
        }
    }

    public void ShowPanel()
    {
        _panelObject.transform.DOScale(1f, 0.2f);
        _panelCanvsGroup.DOFade(1f, 0.2f);
        StartCoroutine(FreezeTime(0.2f, 0.2f));
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioListener.pause = true;

        _panelCanvsGroup.blocksRaycasts = true;
        _isShow = true;
        
        _panelCloseControl.AddPanel(this);
    }

    public void HidePanel()
    {
        Time.timeScale = 1;
        OnClosePanel?.Invoke();
        _panelObject.transform.DOScale(0.8f, 0.2f);
        _panelCanvsGroup.DOFade(0f, 0.2f);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
        
        _panelCanvsGroup.blocksRaycasts = false;
        _isShow = false;
        
        _panelCloseControl.DeprivePanel(this);
    }

    private IEnumerator FreezeTime(float duration, float freezeValue)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = freezeValue;
    }
}
