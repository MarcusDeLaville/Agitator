using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    [SerializeField] private CarStack carStack;
    [SerializeField] private CarInteraction _carInteraction;
    [SerializeField] private EntryInside _entry;
    [SerializeField] private ExitOutside _exit;

    private void OnEnable()
    {
        _carInteraction.PlayerSeating += OnPlayerSeating;
        _carInteraction.PlayerEscaping += OnPlayerEscaping;

        _entry.PlayerSeated += OnPlayerSeated;
        _exit.PlayerEscaped += OnPlayerEscaped;

        // carStack.CarMegafon.enabled = false;
    }

    private void OnDisable()
    {
        _entry.PlayerSeated -= OnPlayerSeated;
        _exit.PlayerEscaped -= OnPlayerEscaped;
    }

    private void OnPlayerSeating(PlayerStack player)
    {
        carStack.Rigidbody.isKinematic = true;
        carStack.DriverDoor.Open();
    }

    private void OnPlayerSeated(EntryInside entry)
    {
        carStack.DriverDoor.Close();
        carStack.CarMovenment.IsWork = true;
        carStack.Rigidbody.isKinematic = false;
        //_car.CarMovenment.enabled = true;
        carStack.CarMegafon.enabled = true;
        // carStack.SmokeObject.SetActive(true);
    }

    private void OnPlayerEscaping(PlayerStack player)
    {
        carStack.DriverDoor.Open();
        carStack.CarMovenment.IsWork = false;
        carStack.Rigidbody.isKinematic = true;
        //_car.CarMovenment.enabled = false;
        carStack.CarMegafon.enabled = false;
        // carStack.SmokeObject.SetActive(false);
    }

    private void OnPlayerEscaped(PlayerStack player)
    {
        carStack.Rigidbody.isKinematic = false;
        carStack.DriverDoor.Close();
    }
}