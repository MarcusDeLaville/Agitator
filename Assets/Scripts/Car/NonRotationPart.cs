using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleControl))]
public class NonRotationPart : MonoBehaviour
{
    [SerializeField] private List<GameObject> _parts;
    
    private VehicleControl _vehicleControl;
    private readonly List<Transform> _wheels = new List<Transform>();

    private void Start()
    {
        _vehicleControl = GetComponent<VehicleControl>();
        _wheels.Add(_vehicleControl.carWheels.wheels.frontLeft);
        _wheels.Add(_vehicleControl.carWheels.wheels.frontRight);
        _wheels.Add(_vehicleControl.carWheels.wheels.backLeft);
        _wheels.Add(_vehicleControl.carWheels.wheels.backRight);
    }

    private void Update()
    {
        if (_parts.Count == 0) return;
        for (int i = 0; i < _parts.Count; i++)
        {
            var yAngle = _wheels[i].eulerAngles;
            _parts[i].transform.eulerAngles = new Vector3(0, yAngle.y, 0);
            // print(yAngle);
            // print(_parts[i].transform.eulerAngles.y);
        }
    }
}