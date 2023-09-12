using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
  // LOOK

  // Get position of head for main camera (ignore rotation)
  [SerializeField] GameObject cam;
  [SerializeField] GameObject cam3rd;
  [SerializeField] Transform pos;

  // Look sensitivity
  [SerializeField] float xSensitivity;
  [SerializeField] float ySensitivity;

  // Look values
  float xLook;
  float yLook;
  float xRotation = 0f;
  int perspective = 1;

  // MOVE

  // Animator component
  Animator anim;

  // Move values
  float xMove;
  float yMove;

  // Status
  [SerializeField]
  PlayerStatus status = new PlayerStatus();

  // Start is called before the first frame update
  void Start()
  {
    anim = GetComponent<Animator>();
    HideCursor();
  }

  // Update is called once per fixed frame
  void FixedUpdate()
  {
    // Main camera inherit position of head (Multi-Position Constraint)
    cam.transform.position = pos.position;
    cam3rd.transform.position = pos.position;

    // Rotate body in World Y-Axis
    transform.Rotate(Vector3.up * xSensitivity * xLook);
    anim.SetFloat("rotate", xLook, 0.1f, Time.deltaTime);

    // Rotate camera in World X-Axis (clamped on localRotation)
    xRotation -= ySensitivity * yLook;
    xRotation = Mathf.Clamp(xRotation, -45f, 45f);
    cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    cam3rd.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

    // Move transition
    anim.SetFloat("x", xMove, 0.25f, Time.deltaTime);
    anim.SetFloat("y", yMove, 0.25f, Time.deltaTime);
  }

  // OnLook is called on every value change and release (clamped rotation velocity)
  void OnLook(InputValue input)
  {
    Vector2 xyInput = input.Get<Vector2>();

    xLook = Mathf.Clamp(xyInput.x, -10f, 10f);
    yLook = Mathf.Clamp(xyInput.y, -10f, 10f);

    //if (xyInput.x != 0)
    //{
    //  anim.SetBool("isLookingHorizontal", true);
    //}
    //else
    //{
    //  anim.SetBool("isLookingHorizontal", false);
    //}
  }

  // OnMove is called on every value change and release
  void OnMove(InputValue input)
  {
    Vector2 xyInput = input.Get<Vector2>();

    xMove = xyInput.x;
    yMove = xyInput.y;
    
    if (xyInput != Vector2.zero)
    {
      anim.SetBool("isMoving", true);
    }
    else
    {
      anim.SetBool("isMoving", false);
    }
  }

  // OnRun is called twice on press and release
  void OnRun()
  {
    bool isRunning = anim.GetBool("isRunning");

    // Hold to run
    if (!isRunning)
    {
      anim.SetBool("isRunning", true);
    }
    else
    {
      anim.SetBool("isRunning", false);
    }
  }

  // OnCrouch is called once on trigger
  void OnCrouch()
  {
    bool isCrouching = anim.GetBool("isCrouching");

    // Press to crouch
    if (!isCrouching)
    {
      anim.SetBool("isCrouching", true);
    }
    else
    {
      anim.SetBool("isCrouching", false);
    }
  }

  // OnPerspective is called once on trigger
  void OnPerspective()
  {
    // Press to crouch
    if (perspective == 1)
    {
      perspective = 3;
      cam.SetActive(false);
      cam3rd.SetActive(true);
    }
    else if (perspective == 3)
    {
      perspective = 1;
      cam3rd.SetActive(false);
      cam.SetActive(true);
    }
  }

  // OnCollisionEnter is called on enter collision
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Ground")
    {
      anim.SetBool("isGrounded", true);
    }

    // GetKey
    if (collision.gameObject.tag.ToLower() == "key")
    {
      status.inventory.AddItem(collision.gameObject.tag.ToLower());
      Destroy(collision.gameObject);
      // Debug.Log("Number of Keys: " + status.inventory.GetNumberOfKeyItem("key").ToString());
    }
  }

  // OnCollisionExit is called on exit collision
  private void OnCollisionExit(Collision collision)
  {
    if (collision.gameObject.tag == "Ground")
    {
      anim.SetBool("isGrounded", false);
    }
  }

  // Custom function to hide cursor on play
  void HideCursor()
  {
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }

  public int AddHp(int s)
  {
        return status.AddHp(s);
  }

    public int GetCurrentHp()
    {
        return status.GetCurrentHp();
    }
}