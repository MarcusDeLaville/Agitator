using UnityEngine;
using UnityEngine.Events;

public class CarDoor : Door
{
    [SerializeField] private CarInteraction _car;
    [SerializeField] private EntryInside _seatAnim;
    [SerializeField] private Place _placeType;
    [SerializeField] private Side _doorSide;

    //private CarInput _input; // разобраться, почему инпут с мувмента не работает

    public event UnityAction<CarDoor, bool> Interacted;

    public Place PlaceType => _placeType;
    public Side DoorSide => _doorSide;
    public EntryInside SeatInCar => _seatAnim;


    protected override void OnEnable()
    {
        base.OnEnable();
        _seatAnim.PlayerSeated += OnPlayerSeated;
       // _input = new CarInput();
        _car.PlayerEscaping += OnPlayerEscape;
       // _input.Doors.OpenCloseDoor.performed += ctx => OpenCloseDoor();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _seatAnim.PlayerSeated -= OnPlayerSeated;
        _car.PlayerEscaping -= OnPlayerEscape;
        //_input.Doors.OpenCloseDoor.performed -= ctx => OpenCloseDoor();
    }

    protected override void OnInteract(PlayerStack player)
    {
        base.OnInteract(null);
        Interacted?.Invoke(this, IsOpen);
    }

    private void OnPlayerSeated(EntryInside seating)
    {
       // _input.Doors.OpenCloseDoor.Enable();
    }

    private void OnPlayerEscape(PlayerStack player)
    {
       // _input.Doors.OpenCloseDoor.Disable(); 
    }

    private void OpenCloseDoor()
    {
        //OnInteract(null);
    }
}

public enum Place
{
    Driver = 0,
    Passenger = 1
}

public enum Side
{
    Left = -1,
    Right = 1
}