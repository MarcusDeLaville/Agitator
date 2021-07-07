using UnityEngine;

public class SwitchHandbrake : MonoBehaviour
{
    public bool HandbrakeState { get; private set; } = true;

    [SerializeField] private bool _handbrake;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            HandbrakeState = !HandbrakeState;
        }

        _handbrake = HandbrakeState;
    }
}