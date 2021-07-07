using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    [SerializeField] private Panel _contextPanel;
    [SerializeField] private KeyCode _openContextPunelButton = KeyCode.Escape;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_openContextPunelButton))
        {
            _contextPanel.ShowPanel();
        }
    }
}
