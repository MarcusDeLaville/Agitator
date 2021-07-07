using UnityEngine;

public class PlayerBillboard : MonoBehaviour
{
    [SerializeField] private GameObject _billboardObject;

    private Transform _mainCameraTransform;
    private Transform _billboardTransform;

    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
        _billboardTransform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        _billboardTransform.LookAt(transform.position + _mainCameraTransform.rotation * Vector3.forward, _mainCameraTransform.rotation * Vector3.up);
    }
}
