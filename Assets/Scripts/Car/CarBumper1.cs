using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBumper1 : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out AnimatorChanger animatorChanger))
        {
            animatorChanger.ChangeAvatar();
        }
    }
}
