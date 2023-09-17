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
  // Moved to be a user option in the MainMenu
  //[SerializeField] float xSensitivity;
  //[SerializeField] float ySensitivity;

    

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
    private float xSensitivity = 0.1f;
    private float ySensitivity = 0.1f;

    // Variables for player footsteps
    private PlayerFootsteps _playerFootsteps;

    private float _sprintVolumeMin = 1f;
    private float _sprintVolumeMax = 3f;
    private float _crouchVolumeMin = 0.1f;
    private float _crouchVolumeMax = 0.2f;

    private float _walkVolumeMin = 0.2f, _walkVolumeMax = 0.6f;

    private float _walkStepDistance = 0.4f;
    private float _sprintStepDistance = 0.25f;
    private float _crouchStepDistance = 0.5f;

    // Status
    [SerializeField]
    PlayerStatus status = new PlayerStatus();

    private void Awake()
    {
        // access the PlayerFootsteps component (script)
        _playerFootsteps = GetComponentInChildren<PlayerFootsteps>();

        if (MainManager.Instance)
        {
            xSensitivity = MainManager.Instance.MouseSensitivity;
            ySensitivity = MainManager.Instance.MouseSensitivity;
        }
    }

    // Start is called before the first frame update
    void Start()
  {
    anim = GetComponent<Animator>();
    HideCursor();

        // Health bar
        UIManager.instance.UpdateHealthBar(PlayerStatus.currentHp, PlayerStatus.maxHp);

        // default volume setting to walk
        _playerFootsteps._volumeMin = _walkVolumeMin;
        _playerFootsteps._volumeMax = _walkVolumeMax;
        _playerFootsteps._stepDistance = _walkStepDistance;
    }

  // Update is called once per fixed frame
  void FixedUpdate()
  {
        if (Cursor.lockState == CursorLockMode.None)
            return;

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

        //xLook = Mathf.Clamp(xyInput.x, -10f, 10f);
        //yLook = Mathf.Clamp(xyInput.y, -10f, 10f);

        xLook = Mathf.Clamp(xyInput.x, -5f, 5f);
        yLook = Mathf.Clamp(xyInput.y, -5f, 5f);

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

        // set volume settings to 'sprint;
        _playerFootsteps._volumeMin = _sprintVolumeMin;
        _playerFootsteps._volumeMax = _sprintVolumeMax;
        _playerFootsteps._stepDistance = _sprintStepDistance;
    }
    else
    {
      anim.SetBool("isRunning", false);

        // set volume settings to 'walk;
        _playerFootsteps._volumeMin = _walkVolumeMin;
        _playerFootsteps._volumeMax = _walkVolumeMax;
        _playerFootsteps._stepDistance = _walkStepDistance;
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

        // set volume settings to 'crouch;
        _playerFootsteps._volumeMin = _crouchVolumeMin;
        _playerFootsteps._volumeMax = _crouchVolumeMax;
        _playerFootsteps._stepDistance = _crouchStepDistance;
    }
    else
    {
      anim.SetBool("isCrouching", false);

        // set volume settings to 'walk;
        _playerFootsteps._volumeMin = _walkVolumeMin;
        _playerFootsteps._volumeMax = _walkVolumeMax;
        _playerFootsteps._stepDistance = _walkStepDistance;
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

    public static void GetHealth(int amount)
    {
        PlayerStatus.currentHp = Mathf.Clamp(PlayerStatus.currentHp + amount, 0, PlayerStatus.maxHp);

        UIManager.instance.UpdateHealthBar(PlayerStatus.currentHp, PlayerStatus.maxHp);
    }

    public void TakeHit(int amount)
    {
        PlayerStatus.currentHp = Mathf.Clamp(PlayerStatus.currentHp - amount, 0, PlayerStatus.maxHp);

        UIManager.instance.UpdateHealthBar(PlayerStatus.currentHp, PlayerStatus.maxHp);

        if (PlayerStatus.currentHp <= 0)
            Die();
    }

    private void Die()
    {
        GameManager.instance.LoseGame();
    }
}

