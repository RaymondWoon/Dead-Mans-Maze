using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAPlayerSprintAndCrouch : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _crouchSpeed;

    // Variables
    private NAPlayerMovement _playerMovement;
    private Transform _lookRoot;

    private float _standHeight;
    private float _crouchHeight = 1.0f;
    private float _moveSpeed;

    private bool _isCrouching;

    // Variables for player footsteps
    private PlayerFootsteps _playerFootsteps;

    private float _sprintVolumeMin = 1f;
    private float _sprintVolumeMax = 3f;
    private float _crouchVolumeMin = 0.1f;
    private float _crouchVolumeMax = 0.2f;

    private float _walkVolumeMin = 0.2f, _walkVolumeMax = 0.6f;

    private float _walkStepDistance = 0.4f;
    private float _sprintStepDistance = 0.25f;
    private float _crouchStepDistance = 0.5f;

    private void Awake()
    {
        // Access the 'PlayerMovement' component (script)
        _playerMovement = GetComponent<NAPlayerMovement>();

        // define the standard 'walking' speed
        _moveSpeed = _playerMovement._speed;

        // define the look root as the first child
        _lookRoot = transform.GetChild(0);

        // set the default height for the player
        _standHeight = _lookRoot.localPosition.y;

        // default is standing
        _isCrouching = false;

        // access the PlayerFootsteps component (script)
        _playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // default volume setting to walk
        _playerFootsteps._volumeMin = _walkVolumeMin;
        _playerFootsteps._volumeMax = _walkVolumeMax;
        _playerFootsteps._stepDistance = _walkStepDistance;
    }

    // Update is called once per frame
    void Update()
    {
        // monitor user input for sprinting and crouching
        Sprint();
        Crouch();
    }

    private void Sprint()
    {
        // sprinting when LeftShift is pressed together with the player moving
        // speed changed to sprint speed
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isCrouching)
        {
            _playerMovement._speed = _sprintSpeed;

            // set volume settings to 'sprint;
            _playerFootsteps._volumeMin = _sprintVolumeMin;
            _playerFootsteps._volumeMax = _sprintVolumeMax;
            _playerFootsteps._stepDistance = _sprintStepDistance;
        }

        // when left shift key is released, speed reverts to walking speed
        if (Input.GetKeyUp(KeyCode.LeftShift) && !_isCrouching)
        {
            _playerMovement._speed = _moveSpeed;

            // set volume settings to 'walk;
            _playerFootsteps._volumeMin = _walkVolumeMin;
            _playerFootsteps._volumeMax = _walkVolumeMax;
            _playerFootsteps._stepDistance = _walkStepDistance;
        }

    }   // end Sprint

    private void Crouch()
    {
        // C toggles crouching.
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_isCrouching)
            {
                // standup and set speed to walk speed
                _lookRoot.localPosition = new Vector3(0f, _standHeight, 0f);
                _playerMovement._speed = _moveSpeed;

                // set volume settings to 'walk;
                _playerFootsteps._volumeMin = _walkVolumeMin;
                _playerFootsteps._volumeMax = _walkVolumeMax;
                _playerFootsteps._stepDistance = _walkStepDistance;

                _isCrouching = false;
            }
            else
            {
                // crouch and set speed to crouch speed
                _lookRoot.localPosition = new Vector3(0f, _crouchHeight, 0f);
                _playerMovement._speed = _crouchSpeed;

                // set volume settings to 'crouch;
                _playerFootsteps._volumeMin = _crouchVolumeMin;
                _playerFootsteps._volumeMax = _crouchVolumeMax;
                _playerFootsteps._stepDistance = _crouchStepDistance;

                _isCrouching = true;
            }
        }

    }   // end Crouch

}
