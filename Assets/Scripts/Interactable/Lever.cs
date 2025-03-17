using UnityEngine;

namespace Interactable {
    public class Lever : InteractionEmitter {
        [SerializeField] private bool _on = false;
        [SerializeField] private float _angle = 20.0f;
        [SerializeField] private float _initialRotation = 0.0f;

        private Quaternion _up;
        private Quaternion _down;
        private Transform _visual;

        private void Start() {
            _visual = transform.GetChild(0);
            _initialRotation += _visual.localRotation.eulerAngles.y;
            _up = Quaternion.AngleAxis(_initialRotation - _angle, _visual.forward);
            _down = Quaternion.AngleAxis(_initialRotation + _angle, _visual.forward);
            _visual.localRotation = _on ? _down : _up;
        }

        private void FixedUpdate() {
            _visual.localRotation = Quaternion.RotateTowards(_visual.localRotation, _on ? _down : _up, 2 * _angle * Time.fixedDeltaTime);
        }

        public override void PlayerTrigger() {
            _on = !_on;
            if (_on) {
                StartInteract(_type);
            } else {
                FinishInteract(_type);
            }
        }
    }
}