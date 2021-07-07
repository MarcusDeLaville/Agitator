using System;
using UnityEngine;

public class DrawPoint : MonoBehaviour
{
    [SerializeField] private float _size = 0.5f;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * _size); 
    }
}
