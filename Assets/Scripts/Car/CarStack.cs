using UnityEngine;

[RequireComponent(typeof(CarMovenment), typeof(Rigidbody))]
public class CarStack : MonoBehaviour
{
    [SerializeField] private DoorCar _driverDoor;
    [SerializeField] private GameObject _smokeObject;
    [SerializeField] private Megafon _carMegafon;
    [SerializeField] private InteractionCar _interactionCar;
    [SerializeField] private float _cameraDistance;

    public DoorCar DriverDoor => _driverDoor;
    public GameObject SmokeObject => _smokeObject;
    public Megafon CarMegafon => _carMegafon;
    public float CameraDistance => _cameraDistance;
    public CarMovenment CarMovenment { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public InteractionCar InteractionCar => _interactionCar;
    private void Awake()
    {
        CarMovenment = GetComponent<CarMovenment>();
        Rigidbody = GetComponent<Rigidbody>();
    }
}