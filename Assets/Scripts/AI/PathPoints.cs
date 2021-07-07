using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[ExecuteAlways]
public class PathPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    [SerializeField] private Color _colorGizmos;
    
    public List<Transform> Points => _points;

    private bool _pathUpdated => _points.Count == gameObject.transform.childCount - 1;

    private void Start()
    {
        PickUpPoints();
    }
    
    private void Update()
    {
        _points.RemoveAll(x => x == null);
        
        if (_pathUpdated == true)
        {
            PickUpPoints();
        }
    }

    [UnityEngine.ContextMenu("PickPoints")]
    private void PickUpPoints()
    {
        _points.Clear();
        _points = GetComponentsInChildren<Transform>().ToList();
        _points.Remove(this.transform); //TODO: rework it crutch for find of type
    }
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < _points.Count; i++)
        {
            Gizmos.color = _colorGizmos;
            Gizmos.DrawSphere(_points[i].position, 0.5f);

            if (i != _points.Count - 1)
            {
                Gizmos.DrawLine(_points[i].position, _points[i + 1].position);
                // Gizmos.DrawLine(_points[i].position, _points[0].position);
            }
            // else
            // {
            //     Gizmos.DrawLine(_points[i].position, _points[i + 1].position);
            // }
        }
    }
}
