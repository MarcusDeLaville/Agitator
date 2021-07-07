using System;
using UnityEngine;

[RequireComponent(typeof(InteractionCar))]
public class CarHandler : MonoBehaviour
{
    [SerializeField] private Radio _radio;
    [SerializeField] private EngineSound _engine;
    private InteractionCar _interactionCar;
    private SwitchHandbrake _switchHandbrake;
    private Megafon _megafon;

    //TODO: Сделать переключение циклом а объекты переключения пометить интерфейсом типа IHandlered или типа того. 
    
    
    private void Awake()
    {
        _interactionCar = GetComponent<InteractionCar>();
        _switchHandbrake = GetComponent<SwitchHandbrake>();
        _megafon = GetComponent<Megafon>();
        _interactionCar.PlayerEntered += OnPlayerEntered;
        _interactionCar.PlayerQuit += OnPlayerQuit;

        _engine.enabled = false;
        _switchHandbrake.enabled = false;
        _megafon.enabled = false;
        _radio.enabled = false;
    }

    private void OnPlayerEntered()
    {
        _switchHandbrake.enabled = true;
        _megafon.enabled = true;
        _radio.enabled = true;
        _engine.enabled = true;
    }

    private void OnPlayerQuit()
    {
        _engine.enabled = false;
        _switchHandbrake.enabled = false;
        _megafon.enabled = false;
        _radio.enabled = false;
    }
}
