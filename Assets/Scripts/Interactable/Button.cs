using UnityEngine;

using Utilities;

namespace Interactable {
    public class Button : InteractionEmitter {
        [SerializeField] private float _resetTime = 1.0f;
        [SerializeField] private CountDownTimer _reset = new CountDownTimer(1.0f);

        private void Start() {
            _reset.Reset();
            _reset.Stop();
            _reset.OnTimerStop += () => FinishInteract(_type);
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
            Debug.Log($"Button {name} was pressed");
            base.PlayerTrigger();
        }
    }
}