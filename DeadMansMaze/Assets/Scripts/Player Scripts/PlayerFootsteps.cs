using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{

    [Header("Footsteps clips")]
    [SerializeField] private AudioClip[] _footstepClip;

    [HideInInspector]
    public float _volumeMin, _volumeMax;

    [HideInInspector]
    public float _stepDistance;

    // Variables
    private CharacterController _characterController;
    private float _accumulatedDistance;
    private AudioSource _footstepSound;

    private void Awake()
    {
        // Initialize to AudioSource component
        _footstepSound = GetComponent<AudioSource>();

        // Initialize to CharacterController component
        _characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // play sound as needed
        CheckToPlayFootstepSound();
    }

    private void CheckToPlayFootstepSound()
    {
        if (!_characterController.isGrounded)
            return;

        if (_characterController.velocity.sqrMagnitude > 0)
        {
            // _accumulatedDistance is the value how far can we go
            // e.g. make a step, sprint, or move while crouching
            // until we play the footstep
            _accumulatedDistance += Time.deltaTime;

            if (_accumulatedDistance > _stepDistance)
            {
                _footstepSound.volume = Random.Range(_volumeMin, _volumeMax);
                _footstepSound.clip = _footstepClip[Random.Range(0, _footstepClip.Length)];
                _footstepSound.Play();

                _accumulatedDistance = 0;
            }
        }
        else
        {
            _accumulatedDistance = 0;
        }

    }   // end CheckToPlayFootstepSound
}
