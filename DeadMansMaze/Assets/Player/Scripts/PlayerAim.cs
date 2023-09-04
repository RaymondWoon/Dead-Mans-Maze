using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
  Camera cam;
  Ray mouseRay;
  Vector3 hitPoint;
  Vector3 currentPoint;

  // Start is called before the first frame update
  void Start()
  {
    cam = Camera.main;
  }

  // Update is called once per frame
  void Update()
  {
    // Raycast from middle of the screen
    Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    mouseRay = cam.ScreenPointToRay(screenCenterPoint);

    // Raycast to aim at colliders or maximum distance from origin
    RaycastHit hitInfo;
    if(Physics.Raycast(mouseRay, out hitInfo, 10f))
    {
      hitPoint = hitInfo.point;
    }
    else
    {
      hitPoint = mouseRay.origin + mouseRay.direction * 10f;
    }

    // Transform position to raycasted destination
    transform.position = hitPoint;
  }
}
