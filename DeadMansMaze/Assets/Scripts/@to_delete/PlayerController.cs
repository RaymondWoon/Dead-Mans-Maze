using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private float _xSensitivity = 2.0f;
    [SerializeField] private float _ySensitivity = 2.0f;
    [SerializeField] private float _minimumX = -70.0f;
    [SerializeField] private float _maximumX = 80.0f;

    [SerializeField] private Animator _anim;

    private Rigidbody _rb;
    private CapsuleCollider _capsule;

    private Quaternion _cameraRot;
    private Quaternion _playerRot;

    private bool _cursorIsLocked = true;
    private bool _lockCursor = true;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capsule = GetComponent<CapsuleCollider>();

        _cameraRot = _camera.transform.localRotation;
        _playerRot = transform.localRotation;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _anim.SetBool("arm", !_anim.GetBool("arm"));
        }
    }

    private void FixedUpdate()
    {
        float yRot = Input.GetAxis("Mouse X") * _ySensitivity;
        float xRot = Input.GetAxis("Mouse Y") * _xSensitivity;

        _cameraRot *= Quaternion.Euler(-xRot, 0, 0);
        _playerRot *= Quaternion.Euler(0, yRot, 0);

        _cameraRot = ClampXRotation(_cameraRot);

        // Update player rotation
        transform.localRotation = _playerRot;
        // update camera rotation
        _camera.transform.localRotation = _cameraRot;

        if (IsGrounded() && Input.GetKey(KeyCode.Space))
        {
            _rb.AddForce(0, 300, 0);
        }
        
        float x = Input.GetAxis("Horizontal") * _speed;
        float z = Input.GetAxis("Vertical") * _speed;

        //transform.position += new Vector3(x * _speed, 0, z * _speed);
        transform.position += _camera.transform.forward * z + _camera.transform.right * x;

        UpdateCursorLock();
    }

    private bool IsGrounded()
    {
        RaycastHit hitInfo;

        if (Physics.SphereCast(transform.position, _capsule.radius, Vector3.down, out hitInfo,
            (_capsule.height / 2.0f) - _capsule.radius + 0.1f))
        {
            return true;
        }

        return false;
    }

    private Quaternion ClampXRotation(Quaternion q)
    {
        // Normalize the Quaternion provided
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, _minimumX, _maximumX);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    private void SetCursorLock(bool value)
    {
        _lockCursor = value;

        if (!_lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void UpdateCursorLock()
    {
        if (_lockCursor)
            InternalLockCursorUpdate();
    }

    private void InternalLockCursorUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _cursorIsLocked = false;
        else if (Input.GetMouseButtonUp(0))
        {
            _cursorIsLocked = true;
        }

        if (_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
