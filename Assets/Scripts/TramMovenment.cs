using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class TramMovenment : MonoBehaviour
{
    [SerializeField] private Transform _tramTansform;
    [SerializeField] private float _speed;
    [SerializeField] private List<Transform> _way = new List<Transform>();
    [SerializeField] private PathType _pathType = PathType.CatmullRom;

    private Tween _pathMoving;

    private void Start()
    {
        CreateWay();
        StartMove();
    }
    
    public void StartMove()
    {
        _pathMoving.SetEase(Ease.Linear).SetLoops(-1);
    }

    private void CreateWay()
    {
        Vector3[] waypoinsVectors = new Vector3[_way.Count];

        for (int i = 0; i < _way.Count; i++)
        {
            waypoinsVectors[i] = _way[i].position;
        }

        _pathMoving = _tramTansform.DOPath(waypoinsVectors, _speed, _pathType)
            .SetOptions(true)
            .SetLookAt(0.001f);
    }
}
