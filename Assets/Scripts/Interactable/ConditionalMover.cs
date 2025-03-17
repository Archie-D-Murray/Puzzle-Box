using UnityEngine;

using Utilities;

namespace Interactable {
    public class ConditionalMover : InteractionReceiver {
        [SerializeField] private Transform[] _positions;
        [SerializeField] private Transform _transform;
        [SerializeField] private int _current = 0;
        [SerializeField] private int _target = 1;
        [SerializeField] private float _transitionTime = 1.0f;
        [SerializeField] private bool _canMove;

        private CountDownTimer _timer;

        private void Start() {
            _timer = new CountDownTimer(_transitionTime);
            _transform.position = _positions[_current].position;
            _transform.rotation = _positions[_current].rotation;
            _transform.localScale = _positions[_current].localScale;
        }

        public override void AcceptInteraction(InteractionSource source) {
            Debug.Log($"Recevied start from {source}");
            _canMove = true;
            _timer.Start();
        }

        public override void CancelInteraction(InteractionSource source) {
            Debug.Log($"Recevied cancel from {source}");
            _canMove = false;
            _timer.Stop();
        }

        private void FixedUpdate() {
            if (!_canMove) { return; }
            _timer.Update(Time.fixedDeltaTime);
            _transform.position = Vector3.Lerp(_positions[_current].position, _positions[_target].position, Mathf.SmoothStep(0.0f, 1.0f, _timer.Progress()));
            _transform.rotation = Quaternion.Slerp(_positions[_current].rotation, _positions[_target].rotation, Mathf.SmoothStep(0.0f, 1.0f, _timer.Progress()));
            _transform.localScale = Vector3.Lerp(_positions[_current].localScale, _positions[_target].localScale, Mathf.SmoothStep(0.0f, 1.0f, _timer.Progress()));
            if (_timer.IsFinished) {
                _current = _target;
                _target = ++_target % _positions.Length;
                _timer.Reset(_transitionTime);
            }
        }
    }
}