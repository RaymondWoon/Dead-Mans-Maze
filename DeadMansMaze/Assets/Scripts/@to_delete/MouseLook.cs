using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAMouseLook : MonoBehaviour
{
    [Header("Root Transforms")]
    [SerializeField] private Transform _playerRoot;
    [SerializeField] private Transform _lookRoot;

    [Header("Mouse Movement")]
    [SerializeField] private float _xSensitivity = 2.0f;
    [SerializeField] private float _ySensitivity = 2.0f;
    [SerializeField] private float _minimumXRot = -70.0f;
    [SerializeField] private float _maximumXRot = 80.0f;

    // Variables
    private Vector2 _lookAngles;
    private Vector2 _currentMouseLook;
    private Vector2 _smoothMove;
    private float _currentRollAngle;
    private int _lastLookFrame;

    // Start is called before the first frame update
    void Start()
    {
        // by default, lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        // monitor if user wants to change the cursor mode
        ToggleCursorMode();

        // use the mouse to look around only if cursor is locked
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    private void ToggleCursorMode()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }   // end ToggleCursorMode

    private void LookAround()
    {
        // current mouse position
        _currentMouseLook = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        // adjust for x sensitivity
        _lookAngles.x += _currentMouseLook.x * _xSensitivity;
        // adjust for y sensitivity
        _lookAngles.y += _currentMouseLook.y * _ySensitivity;

        // clamp the x rotation
        _lookAngles.x = Mathf.Clamp(_lookAngles.x, _minimumXRot, _maximumXRot);

        // update the local rotation
        _lookRoot.localRotation = Quaternion.Euler(-_lookAngles.x, 0f, 0f);
        _playerRoot.localRotation = Quaternion.Euler(0f, _lookAngles.y, 0f);

    }   // end LookAround
}
