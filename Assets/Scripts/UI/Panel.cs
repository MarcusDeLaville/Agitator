using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ShowPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _animator.SetBool("isOpen", true);
        Time.timeScale = 0;
    }

    public void HidePanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _animator.SetBool("isOpen", false);
        Time.timeScale = 1;
    }
}
