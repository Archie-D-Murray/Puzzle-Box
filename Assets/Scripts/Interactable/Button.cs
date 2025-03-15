using UnityEngine;

using Utilities;

namespace Interactable {
    public class Button : InteractionEmitter {
        [SerializeField] private float _resetTime = 1.0f;
        [SerializeField] private CountDownTimer _reset = new CountDownTimer(1.0f);

        private Transform _button;

        private Vector3 _pressed;
        private Vector3 _normal;

        private void Start() {
            _button = transform.GetChild(0);
            _normal = _button.localScale;
            _pressed = _button.localScale;
            _pressed.y *= 0.5f;
            _reset.Reset();
            _reset.Stop();
            _reset.OnTimerStop += Release;
        }

        private void Release() {
            FinishInteract(_type);
            _button.localScale = _normal;
        }

        private void FixedUpdate() {
            _reset.Update(Time.fixedDeltaTime);
            if (_reset.IsRunning && _reset.IsFinished) {
            }
        }

        public override void StartInteract(InteractionSource source) {
            if (_reset.IsRunning) {
                _reset.Reset(_resetTime);
            } else {
                _reset.Reset(_resetTime);
                base.StartInteract(source);
            }
        }

        public override void PlayerTrigger() {
            _button.localScale = _pressed;
            Debug.Log($"Button {name} was pressed");
            base.PlayerTrigger();
        }
    }
}