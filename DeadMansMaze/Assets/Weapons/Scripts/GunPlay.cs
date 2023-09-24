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
  [Header("HUD")]
  [SerializeField] Text Ammo;

  [Header("Weapon")]
  [SerializeField] GameObject Rifle;
  [SerializeField] GameObject Pistol;
  [SerializeField] GameObject RifleMuzzleFlash;
  [SerializeField] GameObject PistolMuzzleFlash;
  [SerializeField] GameObject BulletCasing;
  [SerializeField] Transform RifleEjectPoint;
  [SerializeField] Transform PistolEjectPoint;

  [Header("Weapon Audio")]
  [SerializeField] AudioClip RifleShoot;
  [SerializeField] AudioClip PistolShoot;
  [SerializeField] AudioClip RifleReload;
  [SerializeField] AudioClip PistolReload;
  [SerializeField] AudioClip Holster;

  [Header("Weapon Stats")]
  [SerializeField] int RifleMagCap;
  [SerializeField] int PistolMagCap;
  [SerializeField] float RifleFireRate;
  [SerializeField] float PistolFireRate;

  [Header("Hit Effect")]
  [SerializeField] UnityEvent RifleOnHit;
  [SerializeField] UnityEvent PistolOnHit;

    // Components
    Animator anim;
  RigBuilder rig;
  AudioSource aud;

  // Weapon state
  [HideInInspector]
  public static int RifleBulletLeft;
  public static int PistolBulletLeft;
  public static int WeaponID;
  public static int PistolBulletStock = 30;
  public static int RifleBulletStock = 60;
  bool isReloading;
  bool isFiring = false;
  bool doneFiring = true;

  public static bool _gameIsPaused;

  // Start is called before the first frame update
  void Start()
  {
    anim = GetComponent<Animator>();
    rig = GetComponent<RigBuilder>();
    aud = GetComponent<AudioSource>();

    // Start with Rifle Equiped with Full Mag
    anim.SetInteger("WeaponID", 1);
    RifleBulletLeft = RifleMagCap;
    PistolBulletLeft = PistolMagCap;

    _gameIsPaused = false;
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
    if (_gameIsPaused)
        return;

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
        aud.PlayOneShot(RifleShoot, 0.5f);
        anim.SetTrigger("Fire");
        Instantiate(BulletCasing, RifleEjectPoint.position, RifleEjectPoint.rotation);
        RifleMuzzleFlash.SetActive(true);
        RifleOnHit.Invoke();
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
        aud.PlayOneShot(PistolShoot, 0.5f);
        anim.SetTrigger("Fire");
        Instantiate(BulletCasing, PistolEjectPoint.position, PistolEjectPoint.rotation);
        PistolMuzzleFlash.SetActive(true);
        PistolOnHit.Invoke();
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
    // Revised by Raymond - only allow reload if there are magazine clips available
    if (!isReloading & RifleBulletLeft != RifleMagCap | !isReloading & PistolBulletLeft != PistolMagCap)
    {
        if (WeaponID == 1 && RifleBulletStock > 0)
        {
            anim.SetBool("isReloading", true);
            aud.PlayOneShot(RifleReload, 0.5f);
        }
        else if (WeaponID == 2 && PistolBulletStock > 0)
        {
            anim.SetBool("isReloading", true);
            aud.PlayOneShot(PistolReload, 0.5f);
        }
    }
  }

  // OnSwitch is called once on trigger (stops reloading)
  void OnSwitch()
  {
    aud.PlayOneShot(Holster, 0.5f);

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
            //RifleBulletLeft = RifleMagCap;

            // If out of bullets, exit
            if (RifleBulletStock <= 0)
                    return;

            if (RifleBulletStock >= RifleMagCap)
            {
                RifleBulletStock -= RifleMagCap;
            }
            else
            {
                RifleBulletStock = 0;
            }

            RifleBulletLeft = RifleMagCap;

        }
    else if (WeaponID == 2)
    {
            //PistolBulletLeft = PistolMagCap;

            // If out of bullets, exit
            if (PistolBulletStock <= 0)
                return;

            if (PistolBulletStock >= PistolMagCap)
            {
                PistolBulletStock -= PistolMagCap;
            }
            else
            {
                PistolBulletStock = 0;
            }

            PistolBulletLeft = PistolMagCap;
    }
  }
}
