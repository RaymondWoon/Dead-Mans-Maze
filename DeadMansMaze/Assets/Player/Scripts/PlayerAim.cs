using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
  // Shot 
  public ParticleSystem AimHit;

  // Shoot
  Vector3 hitPoint;
  Vector3 hitDirection;
  bool Surface = false;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // Raycast from middle of the screen
    Camera cam = Camera.main;
    Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    Ray mouseRay = cam.ScreenPointToRay(screenCenterPoint);

    // Raycast to aim at colliders or maximum distance from origin
    RaycastHit hitInfo;
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

  public void HitEffect()
  {
    if (Surface)
    {
      AimHit.transform.position = hitPoint;
      AimHit.transform.forward = hitDirection;
      AimHit.Emit(1);
    }
  }
}
