using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAPlayerAttack : MonoBehaviour
{
    public float _fireRate = 15f;
    private float _nextShootTime;
    public float _damage = 20f;

    // Variables
    private NAWeaponManager _weaponManager;

    private void Awake()
    {
        _nextShootTime = 0.0f;
        _weaponManager = GetComponent<NAWeaponManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
    }

    private void WeaponShoot()
    {
        // assault rifle
        if (_weaponManager.GetCurrentSelectedWeapon()._fireType == WeaponFireType.MULTIPLE)
        {
            if (Input.GetMouseButton(0) && Time.time > _nextShootTime)
            {
                _nextShootTime = Time.time + 1.0f / _fireRate;

                _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                //Bullets fired();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                // bulletfired
            }


        }
    }
}
