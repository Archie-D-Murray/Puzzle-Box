using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Transform _camera;

    [Header("Movement")]
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _turnSpeed = 45.0f;
    [SerializeField] private Vector3 _direction;

    private Rigidbody _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {

    }

    private void FixedUpdate() {
        Vector3 move = Input.GetAxisRaw("Horizontal") * Vector3.right + Input.GetAxisRaw("Vertical") * Vector3.forward;
        move.y = 0.0f;
        Vector3.ClampMagnitude(move, 1f);

        _direction = Quaternion.AngleAxis(_camera.transform.eulerAngles.y, Vector3.up) * move;
        if (move.sqrMagnitude > 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction), _turnSpeed * Time.fixedDeltaTime);
        }
        _rb.velocity = _direction * _speed;
    }

    private void LateUpdate() {

    }
}