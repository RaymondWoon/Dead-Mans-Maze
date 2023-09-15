using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAPlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 10.0f;

    // Variables
    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private const float GRAVITY = 9.8f;
    private float _verticalVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

    }   // end Awake

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

    }   // end Update

    private void MovePlayer()
    {
        // get the move direction based on the user input
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        // transform to Unity world space
        _moveDirection = transform.TransformDirection(_moveDirection);
        // adjust for speed
        _moveDirection *= _speed * Time.deltaTime;

        // adjust the movedirection y component if required
        ApplyGravity();

        // apply to CharacterController
        _characterController.Move(_moveDirection);

    }   // end MovePlayer

    private void ApplyGravity()
    {
        // adjust the vertical velocity due to the effect of gravity
        _verticalVelocity -= GRAVITY * Time.deltaTime;

        // check if player request to jump
        // character is grounded and user pressed the spacebar
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
            _verticalVelocity = _jumpForce;
        
        // adjust the y component
        _moveDirection.y = _verticalVelocity * Time.deltaTime;

    }   // end ApplyGravity

}
