using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private readonly float _speed = 0.1f;
    private float _xSensitivity = 2f;
    private float _ySensitivity = 2f;
    private float _minimumX = -90f;
    private float _maximumX = 90f;
    private Rigidbody _rb;
    CapsuleCollider _capsuleCollider;

    [SerializeField]
    private GameObject _camera;


    private Quaternion _cameraRot;
    private Quaternion _playerRot;

    private float _x;
    private float _z;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        _cameraRot = _camera.transform.localRotation;
        _playerRot = transform.localRotation;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float yRot = Input.GetAxis("Mouse X") * _ySensitivity;
        float xRot = Input.GetAxis("Mouse Y") * _xSensitivity;

        _cameraRot *= Quaternion.Euler(xRot * -1.0f, 0, 0);
        _playerRot *= Quaternion.Euler(0, yRot, 0);

        _cameraRot = ClampRotationAroundXAxis(_cameraRot);

        transform.localRotation = _playerRot;
        _camera.transform.localRotation = _cameraRot;

        _x = Input.GetAxis("Horizontal") * _speed;
        _z = Input.GetAxis("Vertical") * _speed;

        transform.position += _camera.transform.forward * _z + _camera.transform.right * _x;    // +new Vector3(_x * _speed, 0, _z * _speed);

    }

    private Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        // normalize q
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        // convert the X value of the quaternion to Euler  X value
        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        // clamp the angle between the min and max
        angleX = Mathf.Clamp(angleX, _minimumX, _maximumX);
        // takes the clamped Euler angle  and turns it back to a Quaternion
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
