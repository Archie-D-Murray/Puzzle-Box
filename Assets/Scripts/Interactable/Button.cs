using UnityEngine;

using Utilities;

namespace Interactable {
    public class Button : InteractionEmitter {
        [SerializeField] private float _resetTime = 1.0f;
        [SerializeField] private CountDownTimer _reset = new CountDownTimer(1.0f);

        private void FixedUpdate() {
            _reset.Update(Time.fixedDeltaTime);
            if (_reset.IsFinished) {
                FinishInteract(_type);
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

        private void OnTriggerEnter(Collider collider) {
            if (PlayerInputs.Instance.Interaction) {
                StartInteract(_type);
            }
        }
    }
}