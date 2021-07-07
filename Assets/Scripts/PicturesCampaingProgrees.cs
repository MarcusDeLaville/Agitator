using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ProgressPicture
{
    public Sprite Picture;
    public float ProgressProcent;
    public bool IsShowed;
}

public class PicturesCampaingProgrees : MonoBehaviour
{
    [SerializeField] private List<ProgressPicture> _progressPictures;
    [SerializeField] private CitizenFractionCounter _citizenFractionCounter;
    [SerializeField] private Image _pictureRenderer;
    [SerializeField] private ContextPanelAnimation _picturePanel;
    
    private void Awake()
    {
        StartCoroutine(EnableCheakingProgress());
    }

    private IEnumerator EnableCheakingProgress()
    {
        yield return new WaitForSeconds(8f);
        _citizenFractionCounter.ParticipantAdded += CheakAgitationProgress;
        CheakAgitationProgress();
    }

    private void CheakAgitationProgress()
    {
        float pioners = _citizenFractionCounter.Fractions[1].ParticipantsFraction.Count;
        float oneProcentParticipants = (float)_citizenFractionCounter.AllParticipants.Count / 100;
        float procent = pioners / oneProcentParticipants;
        // Debug.Log($"Пионеры: {pioners}|Один процент: {oneProcentParticipants}|Процент успеха: {procent}|");
        
        ShowPicture(procent);
    }

    private void ShowPicture(float procents)
    {
        for (int i = 0; i < _progressPictures.Count; i++)
        {
            if (procents >= _progressPictures[i].ProgressProcent)
            {
                if (_progressPictures[i].IsShowed == false)
                {
                    _pictureRenderer.sprite = _progressPictures[i].Picture;
                    _picturePanel.ShowPanel();
                    _progressPictures[i].IsShowed = true;
                    // Debug.Log(_progressPictures[i].Picture.name);
                }
            }
        }
    }
}
