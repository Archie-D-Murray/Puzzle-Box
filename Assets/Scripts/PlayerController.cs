using UnityEngine;

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

    readonly Vector3 _extents = new Vector3 { x = 0.25f, y = 0.1f, z = 0.25f };

    private Rigidbody _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _groundLayer = 1 << LayerMask.NameToLayer("Ground");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && (_grounded || _coyoteTimer < _coyoteTime)) {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate() {
        _grounded = GetGrounded();
        Vector3 move = Input.GetAxisRaw("Horizontal") * Vector3.right + Input.GetAxisRaw("Vertical") * Vector3.forward;
        move.y = 0.0f;
        Vector3.ClampMagnitude(move, 1f);

        _direction = Quaternion.AngleAxis(_camera.transform.eulerAngles.y, Vector3.up) * move;
        if (move.sqrMagnitude > 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction), Time.fixedDeltaTime * _turnSpeed);
        }
        if (_grounded) {
            _rb.velocity = _direction * _speed;
            _coyoteTimer = 0.0f;
        } else {
            _rb.velocity += Vector3.down * _fallSpeed;
            _coyoteTimer += Time.fixedDeltaTime;
        }
    }

    private bool GetGrounded() {
        return Physics.CheckSphere(transform.position, 0.45f, _groundLayer);
    }
}