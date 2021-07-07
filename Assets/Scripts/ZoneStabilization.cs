using UnityEngine;

public class ZoneStabilization : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private float _distance;

    private Transform _targetTransform;
    
    private void Update()
    {
        CheckGround();

        if (_targetObject != null)
        {
            _targetTransform.position = _targetObject.transform.position;
            _targetTransform.rotation = Quaternion.Euler(-90, 0, 0);
            
            gameObject.transform.LookAt(_targetObject.transform);
        }
    }

    private void CheckGround()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, _distance, _layerMask))
        {
            Debug.DrawRay(transform.position + Vector3.up, Vector3.down * hit.distance, Color.blue);
        }
        
        if (hit.collider != null)
        {
            
            _targetObject = hit.collider.gameObject;
           
        }
    }
}
