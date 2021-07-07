using System;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float _delay = 35f;

    private void Start()
    {
        Destroy(gameObject, _delay);
    }

    private void Update()
    {
        if(transform.position.y < -500)
        {
            Destroy(gameObject);
        }
    }
}
