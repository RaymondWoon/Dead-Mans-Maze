using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assault Rifle can fire multiple bullets
public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public class WeaponHandler : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject _muzzleFlash;
    public WeaponFireType _fireType;

    [Header("Sound")]
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _reloadSound;

    // Variables
    private Animator _anim;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void ShootAnimation()
    {
        _anim.SetTrigger("shoot");
    }

    private void Aim(bool canAim)
    {
        _anim.SetBool("aim", canAim);
    }

    private void TurnOnMuzzleFlash()
    {
        _muzzleFlash.SetActive(true);
    }

    private void TurnOffMuzzleFlash()
    {
        _muzzleFlash.SetActive(false);
    }


    private void PlayShootSound()
    {
        _shootSound.Play();
    }

    private void PlayReloadSound()
    {
        _reloadSound.Play();
    }

}
