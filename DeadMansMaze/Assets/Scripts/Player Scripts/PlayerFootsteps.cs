using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{

    [Header("Footsteps clips")]
    [SerializeField] private AudioClip[] _footstepClip;

    [SerializeField] private Animator _anim;

    [HideInInspector]
    public float _volumeMin, _volumeMax;

    [HideInInspector]
    public float _stepDistance;

    // Variables
    private float _accumulatedDistance;
    private AudioSource _footstepSound;

    private void Awake()
    {
        // Initialize to AudioSource component
        _footstepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // play sound as needed
        CheckToPlayFootstepSound();
    }

    private void CheckToPlayFootstepSound()
    {
        if (!_anim.GetBool("isMoving"))
            return;

        if (_anim.velocity.sqrMagnitude > 0)
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
