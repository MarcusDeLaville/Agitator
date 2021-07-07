using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarMovenment : MonoBehaviour
{
    public bool IsWork = false;
    public bool IsBraking = false;

    [SerializeField] private float _maximumAcceleration = 20f;
    [SerializeField] private float _turnSensivity = 1f;
    [SerializeField] private float _maximumSteerAngle = 45f;
    [SerializeField] private float _brakeForse = 10000;
    [SerializeField] private float _handbrakeBrakeForse = 35000;
    [SerializeField] private List<Wheel> _wheels;
    [SerializeField] private List<Wheel> _driveWheels;

    [SerializeField] private KeyCode _brakeButton = KeyCode.Space;
    [SerializeField] private SwitchHandbrake _switchHandbrake;

    [SerializeField] private CarDrive _carDrive;

    [SerializeField] private float _brakeMultiplierForward;
    [SerializeField] private float _brakeMultiplierBackword;
    [SerializeField] private bool _isBrake;
    [SerializeField] private bool _haveSupports = false;

    [SerializeField] private int _vectorMultiplier = -1;

    //for non rotation
    [SerializeField] private Transform[] _wheel = new Transform[4];
    [SerializeField] private Transform[] _stopWheel = new Transform[4];

    private void Start()
    {
        FindDriveWheels();
    }

    private void OnValidate()
    {
        FindDriveWheels();
    }

    private void Update()
    {
        DrawWheels();
    }

    private void FixedUpdate()
    {
        if (IsWork == true)
        {
            Move();
        }

        if (Input.GetKey(_brakeButton))
        {
            _isBrake = true;
        }
        else
        {
            _isBrake = false;
        }

        BrakeMove();
    }

    private void StopRotation()
    {
        for (int i = 0; i < _wheel.Length; i++)
        {
            var rotation = _wheel[i].rotation;
            var quart = new Quaternion();
            quart.Set(0, rotation.y, 0, rotation.w);
            _stopWheel[i].rotation = quart;
        }
    }

    private void BrakeMove()
    {
        foreach (Wheel wheel in _wheels)
        {
            if (_switchHandbrake.HandbrakeState == false)
            {
                if (_isBrake == true)
                {
                    if (wheel.Axel == Axel.Front)
                    {
                        _brakeMultiplierForward = Mathf.Lerp(_brakeMultiplierForward, 1, 0.5f);
                        wheel.WheelCollider.brakeTorque = _brakeForse * _brakeMultiplierForward;
                    }

                    if (wheel.Axel == Axel.Rear)
                    {
                        _brakeMultiplierBackword = Mathf.Lerp(_brakeMultiplierBackword, 1, 1.5f);
                        wheel.WheelCollider.brakeTorque = _brakeForse * _brakeMultiplierBackword;
                    }

                    IsBraking = true;
                }
                else
                {
                    _brakeMultiplierForward = 0;
                    _brakeMultiplierBackword = 0;
                    wheel.WheelCollider.brakeTorque = _brakeForse * 0;

                    IsBraking = false;
                }
            }
            else
            {
                if (wheel.Axel == Axel.Rear)
                {
                    wheel.WheelCollider.brakeTorque = _handbrakeBrakeForse;
                    IsBraking = true;
                }
            }
        }
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical") * _vectorMultiplier;
        // float vertical = Input.GetAxis("Vertical") * 1;
        float horizontal = Input.GetAxis("Horizontal");

        foreach (Wheel wheel in _driveWheels)
        {
            wheel.WheelCollider.motorTorque = vertical * _maximumAcceleration * 500 * Time.deltaTime;

            if (wheel.Axel == Axel.Front)
            {
                float steerAngle = horizontal * _turnSensivity * _maximumSteerAngle;
                wheel.WheelCollider.steerAngle = Mathf.Lerp(wheel.WheelCollider.steerAngle, steerAngle, 0.5f);
            }
        }
    }

    private void DrawWheels()
    {
        foreach (Wheel wheel in _wheels)
        {
            Quaternion rotation;
            Vector3 position;

            wheel.WheelCollider.GetWorldPose(out position, out rotation);
            wheel.WheelModel.transform.position = position;
            wheel.WheelModel.transform.rotation = rotation;
        }

        foreach (var wheel in _driveWheels)
        {
            if (_haveSupports == true)
            {
                var currentRotation = wheel.WheelModel.transform.rotation;
                var quart = new Quaternion();
                quart.Set(0, currentRotation.y, 0, currentRotation.w);
                wheel.NotRoatationPart.transform.rotation = quart;
            }
        }
    }

    private void FindDriveWheels()
    {
        _driveWheels.Clear();

        if (_carDrive == CarDrive.Backward)
        {
            _driveWheels.AddRange(_wheels.Where(wheel => wheel.Axel == Axel.Rear));
        }
        else if (_carDrive == CarDrive.Forward)
        {
            _driveWheels.AddRange(_wheels.Where(wheel => wheel.Axel == Axel.Front));
        }
        else
        {
            _driveWheels.AddRange(_wheels);
        }
    }
}

public enum Axel
{
    Front,
    Rear
}

public enum CarDrive
{
    Forward,
    Backward,
    Full
}

[Serializable]
public struct Wheel
{
    public GameObject WheelModel;
    public GameObject NotRoatationPart;
    public WheelCollider WheelCollider;
    public Axel Axel;
}