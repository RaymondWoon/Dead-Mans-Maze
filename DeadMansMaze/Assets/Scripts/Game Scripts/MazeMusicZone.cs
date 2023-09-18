using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMusicZone : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _fadeTime;
    [SerializeField] private GameObject _door;
    
    // Variables
    private float _targetVolume;

    private void Start()
    {
        // Initialize 
        _targetVolume = 0.0f;
        _audioSource.volume = 0.0f;
    }

    private void Update()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, (1.0f / _fadeTime) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _targetVolume = 1.0f;
            //_door.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _targetVolume = 0.0f;
        }
    }
}
