using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject _fpsCamera;
    [SerializeField] private GameObject _tpsCamera;
    [SerializeField] private GameObject _carCamera;
    [SerializeField] private GameObject _fpsCarCamera;

    [SerializeField] private PlayerStack _playerStack;

    private void Start()
    {
       //_playerStack.PlayerMovenment.Input.Interactive.CameraSwitch.performed += ctx => Switch();
    }

    private void Switch()
    {
        if (_fpsCamera.activeSelf == true | _fpsCarCamera.activeSelf == true)
        {
            _fpsCamera.SetActive(false);
            _fpsCarCamera.SetActive(false);

            if (_playerStack.InCar == false)
            {
                _tpsCamera.SetActive(true);
            }
            else
            {
                _carCamera.SetActive(true);
            }
        }
        else
        {
            if (_playerStack.InCar == false)
            {
                _fpsCamera.SetActive(true);
                _tpsCamera.SetActive(false);
            }
            else
            {
                _fpsCarCamera.SetActive(true);
                _tpsCamera.SetActive(false);
            } 
        }
    }
}
