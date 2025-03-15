using UnityEngine;

using Utilities;

namespace Interactable {
    public class Moveable : InteractionReceiver {
        [SerializeField] private Transform[] _positions;
        [SerializeField] private Transform _transform;
        [SerializeField] private int _current = 0;
        [SerializeField] private int _target = 0;
        [SerializeField] private float _transitionTime = 1.0f;

        private CountDownTimer _timer;

        private void Start() {
            _timer = new CountDownTimer(_transitionTime);
            _transform.position = _positions[0].position;
            _transform.rotation = _positions[0].rotation;
            _transform.localScale = _positions[0].localScale;
        }

        public override void AcceptInteraction(InteractionSource source) {
            Debug.Log($"Recevied start from {source}");
            _target = ++_target % _positions.Length;
            _timer.Reset(_transitionTime);
        }

        public override void CancelInteraction(InteractionSource source) {
            Debug.Log($"Recevied cancel from {source}");
            _target--;
            if (_target < 0) {
                _target += _positions.Length;
            }
            _timer.Reset(_transitionTime);
        }

        private void FixedUpdate() {
            _timer.Update(Time.fixedDeltaTime);
            if (_timer.IsFinished) {
                _current = _target;
                return;
            }
            if (_current != _target && _timer.IsRunning) {
                _transform.position = Vector3.Lerp(_positions[_current].position, _positions[_target].position, Mathf.SmoothStep(0.0f, 1.0f, _timer.Progress()));
                _transform.rotation = Quaternion.Slerp(_positions[_current].rotation, _positions[_target].rotation, Mathf.SmoothStep(0.0f, 1.0f, _timer.Progress()));
                _transform.localScale = Vector3.Lerp(_positions[_current].localScale, _positions[_target].localScale, Mathf.SmoothStep(0.0f, 1.0f, _timer.Progress()));
            }
        }
    }
}