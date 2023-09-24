using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
  // Shot
  public int RifleDamage;
  public int PistolDamage;
  public ParticleSystem AimHit;

  // Shoot
  Vector3 hitPoint;
  Vector3 hitDirection;
  RaycastHit hitInfo;
  bool Surface = false;

  // Start is called before the first frame update
  void Start()
  {
        //_game_ui
  }

  // Update is called once per frame
  void Update()
  {
        if (GameUI_Manager.instance._currentState == GameUI_Manager.GameUI_State.Pause)
            return;

    // Raycast from middle of the screen
    Camera cam = Camera.main;
    Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    Ray mouseRay = cam.ScreenPointToRay(screenCenterPoint);

        // Raycast to aim at colliders or maximum distance from origin
        if (Physics.Raycast(mouseRay, out hitInfo, 10f))
        {
            hitPoint = hitInfo.point;
            hitDirection = hitInfo.normal;
            Surface = true;
        }
        else
        {
            hitPoint = mouseRay.origin + mouseRay.direction * 10f;
            Surface = false;
        }

        // Transform position to raycasted destination
        transform.position = hitPoint;
    }

  public void HitEffectRifle()
  {
        
    if (Surface)
    {
      AimHit.transform.position = hitPoint;
      AimHit.transform.forward = hitDirection;
      AimHit.Emit(1);
      if (hitInfo.collider.tag == "Enemy")
      {
        Debug.Log("RifleDamage");
        hitInfo.collider.gameObject.GetComponent<EnemyController>().DamageEnemy(RifleDamage);
      }
    }
  }

  public void HitEffectPistol()
  {
        
        if (Surface)
    {
      AimHit.transform.position = hitPoint;
      AimHit.transform.forward = hitDirection;
      AimHit.Emit(1);
      if (hitInfo.collider.tag == "Enemy")
      {
        Debug.Log("PistolDamage");
        hitInfo.collider.gameObject.GetComponent<EnemyController>().DamageEnemy(PistolDamage);
      }
    }
  }
}
