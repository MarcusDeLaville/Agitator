using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public bool IsCameraRotate { get; private set; } = false;

    [SerializeField] private Transform _followObject;
    [SerializeField] private float _sensivity;
    [SerializeField] private KeyCode _rotateButton = KeyCode.LeftAlt;
    [SerializeField] private float _smooth = 1.5f;

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensivity;

        if (Input.GetKey(_rotateButton))
        {
            _followObject.localEulerAngles += new Vector3(0, mouseX, 0);
            IsCameraRotate = true;
        }
        else
        {
            _followObject.localEulerAngles = Vector3.Lerp(_followObject.localEulerAngles, Vector3.zero, _smooth * Time.deltaTime);
            IsCameraRotate = false;
        }
    }
}
