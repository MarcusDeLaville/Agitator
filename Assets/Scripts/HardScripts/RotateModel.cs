using System;
using DG.Tweening;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    [SerializeField] private float _duration = 3;
    
    private Transform _transformModel;

    private void Awake()
    {
        _transformModel = GetComponent<Transform>();
        _transformModel.DORotate(new Vector3(0, 360, 0), _duration,  RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental);
    }
}
