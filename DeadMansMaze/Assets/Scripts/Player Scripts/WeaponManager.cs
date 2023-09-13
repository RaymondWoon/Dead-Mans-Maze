using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private WeaponHandler[] _weapons;


    private int _currentWeaponIndex;

    private void Awake()
    {
        _currentWeaponIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        _weapons[_currentWeaponIndex].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateSelectedWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateSelectedWeapon(2);
        }

    }   // update

    private void ActivateSelectedWeapon(int weaponIndex)
    {
        if (_currentWeaponIndex == weaponIndex)
            return;

        // Deactivate current weapon
        _weapons[_currentWeaponIndex].gameObject.SetActive(false);

        // Activate required weapon
        _weapons[weaponIndex].gameObject.SetActive(true);

        // update _currentWeaponIndex
        _currentWeaponIndex = weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return _weapons[_currentWeaponIndex];
    }

}
