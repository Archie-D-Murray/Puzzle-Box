using UnityEngine;

using Utilities;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform _camera;

    [Header("Movement")]
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _fallSpeed = 1.0f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _turnSpeed = 45.0f;
    [SerializeField] private bool _grounded;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _coyoteTime = 0.25f;
    [SerializeField] private float _coyoteTimer = 0.0f;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Camera")]
    [SerializeField] private Vector2 _mouseDelta;
    [SerializeField] private float _thresholdLook = 0.01f;
    [SerializeField] private float _xSensitivity = 1.0f;
    [SerializeField] private float _ySensitivity = -1.0f;
    ///<summary>Euler Y rotation => uses mouseX to offset</summary>
    [SerializeField] private float _targetCamYaw;
    ///<summary>Euler X rotation => uses mouseY to offset</summary>
    [SerializeField] private float _targetCamPitch;
    [SerializeField] private float _topClamp = 70.0f;
    [SerializeField] private float _bottomClamp = -30.0f;
    [SerializeField] private float _cameraAngleOverride = 0.0f;

    readonly Vector3 _extents = new Vector3 { x = 0.25f, y = 0.1f, z = 0.25f };

    private Rigidbody _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _groundLayer = 1 << LayerMask.NameToLayer("Ground");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _targetCamYaw = _camera.transform.eulerAngles.y;
    }

    private void Update() {
        if (PlayerInputs.Instance.Jump && (_grounded || _coyoteTimer < _coyoteTime)) {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        _mouseDelta = PlayerInputs.Instance.Look;
    }

    private void FixedUpdate() {
        _grounded = GetGrounded();
        Vector3 move = PlayerInputs.Instance.Move.x * Vector3.right + PlayerInputs.Instance.Move.y * Vector3.forward;
        move.y = 0.0f;
        Vector3.ClampMagnitude(move, 1f);

        _direction = Quaternion.AngleAxis(_camera.transform.eulerAngles.y, Vector3.up) * move;
        if (move.sqrMagnitude > 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction), Time.fixedDeltaTime * _turnSpeed);
        }
        if (_grounded) {
            _rb.velocity = _direction * (_speed * (PlayerInputs.Instance.Sprint ? 1.5f : 1.0f));
            _coyoteTimer = 0.0f;
        } else {
            _rb.velocity += Vector3.down * _fallSpeed;
            _coyoteTimer += Time.fixedDeltaTime;
        }
    }

    private void LateUpdate() {
        Camera();
    }

    private bool GetGrounded() {
        return Physics.CheckSphere(transform.position, 0.45f, _groundLayer);
    }

    private void Camera() {
        // if there is an input and camera position is not fixed
        if (_mouseDelta.sqrMagnitude >= _thresholdLook) {
            _targetCamYaw += _mouseDelta.x * _xSensitivity;
            _targetCamPitch += _mouseDelta.y * _ySensitivity;
        }

        // clamp our rotations so our values are limited 360 degrees
        _targetCamYaw = Helpers.ClampAngle(_targetCamYaw, float.MinValue, float.MaxValue);
        _targetCamPitch = Helpers.ClampAngle(_targetCamPitch, _bottomClamp, _topClamp);

        // Cinemachine will follow this target
        _camera.transform.rotation = Quaternion.Euler(_targetCamPitch + _cameraAngleOverride, _targetCamYaw, 0.0f);
    }
}