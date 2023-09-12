using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using UnityEngine.Events;

public class GunPlay : MonoBehaviour
{
  // Weapon
  [SerializeField] Text Ammo;
  [SerializeField] GameObject Rifle;
  [SerializeField] GameObject Pistol;
  [SerializeField] GameObject RifleMuzzleFlash;
  [SerializeField] GameObject PistolMuzzleFlash;
  [SerializeField] GameObject BulletCasing;
  [SerializeField] Transform RifleEjectPoint;
  [SerializeField] Transform PistolEjectPoint;
  [SerializeField] int RifleMagCap;
  [SerializeField] int PistolMagCap;
  [SerializeField] float RifleFireRate;
  [SerializeField] float PistolFireRate;
  [SerializeField] UnityEvent OnHit;

  // Components
  Animator anim;
  RigBuilder rig;

  // Weapon state
  float RifleBulletLeft;
  float PistolBulletLeft;
  int WeaponID;
  bool isReloading;
  bool isFiring = false;
  bool doneFiring = true;

  // Start is called before the first frame update
  void Start()
  {
    anim = GetComponent<Animator>();
    rig = GetComponent<RigBuilder>();

    anim.SetInteger("WeaponID", 1);
    RifleBulletLeft = RifleMagCap;
    PistolBulletLeft = PistolMagCap;
  }

  // Update is called once per frame
  void Update()
  {
    WeaponID = anim.GetInteger("WeaponID");
    isReloading = anim.GetBool("isReloading");

    if (WeaponID == 1)
    {
      Ammo.text = "RIFLE AMMO\n" + RifleBulletLeft + " / " + RifleMagCap;
    }
    else if (WeaponID == 2)
    {
      Ammo.text = "PISTOL AMMO\n" + PistolBulletLeft + " / " + PistolMagCap;
    }
  }

  // OnShoot is called twice on press and release
  void OnFire()
  {
    // Hold to shoot
    if (!isFiring)
    {
      isFiring = true;
      
      // Shoot only if not already shooting or not reloading
      if (!isReloading && doneFiring)
      {
        StartCoroutine(Firing());
      }
    }
    else
    {
      isFiring = false;
    }
  }

  // Firing is called continuously if is firing every delay
  IEnumerator Firing()
  {
    if (WeaponID == 1)
    {
      if (RifleBulletLeft > 0)
      {
        RifleBulletLeft--;
        anim.SetTrigger("Fire");
        Instantiate(BulletCasing, RifleEjectPoint.position, RifleEjectPoint.rotation);
        RifleMuzzleFlash.SetActive(true);
        OnHit.Invoke();
        doneFiring = false;
        yield return new WaitForSeconds(RifleFireRate);
        RifleMuzzleFlash.SetActive(false);
        doneFiring = true;
        if (isFiring)
        {
          StartCoroutine(Firing());
        }
      }
      if (RifleBulletLeft == 0)
      {
        OnReload();
      }
    }
    else if (WeaponID == 2)
    {
      if (PistolBulletLeft > 0)
      {
        PistolBulletLeft--;
        anim.SetTrigger("Fire");
        Instantiate(BulletCasing, PistolEjectPoint.position, PistolEjectPoint.rotation);
        PistolMuzzleFlash.SetActive(true);
        OnHit.Invoke();
        doneFiring = false;
        yield return new WaitForSeconds(PistolFireRate);
        PistolMuzzleFlash.SetActive(false);
        doneFiring = true;
        if (isFiring)
        {
          StartCoroutine(Firing());
        }
      }
      if (PistolBulletLeft == 0)
      {
        OnReload();
      }
      }
  }

  // OnReload is called once on trigger or when mag is empty
  void OnReload()
  {
    // Reload only if not already reloading or mag is not full
    if(!isReloading & RifleBulletLeft != RifleMagCap | PistolBulletLeft != PistolMagCap)
    {
      anim.SetBool("isReloading", true);
    }
  }

  // OnSwitch is called once on trigger (stops reloading)
  void OnSwitch()
  {
    if (WeaponID == 1)
    {
      anim.SetBool("isReloading", false);
      Rifle.SetActive(false);
      Pistol.SetActive(true);
      rig.layers[1].active = false;
      rig.layers[2].active = false;
      rig.layers[4].active = true;
      rig.layers[5].active = true;
      anim.SetInteger("WeaponID", 2);
    }
    else if (WeaponID == 2)
    {
      anim.SetBool("isReloading", false);
      Rifle.SetActive(true);
      Pistol.SetActive(false);
      rig.layers[1].active = true;
      rig.layers[2].active = true;
      rig.layers[4].active = false;
      rig.layers[5].active = false;
      anim.SetInteger("WeaponID", 1);
    }
  }

  // Reloaded is called once the animation done reloading
  public void Reloaded()
  {
    anim.SetBool("isReloading", false);

    if (WeaponID == 1)
    {
      RifleBulletLeft = RifleMagCap;
    }
    else if (WeaponID == 2)
    {
      PistolBulletLeft = PistolMagCap;
    }
  }
}