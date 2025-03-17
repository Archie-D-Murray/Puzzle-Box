using UnityEngine;

namespace Interactable {

    public class ConditionalReceiver : InteractionReceiver {
        public enum ConditionMode { Or, And, Xor }
        [SerializeField] private int _maxSignals = 2;
        [SerializeField] private int _currentSignals = 0;
        [SerializeField] private InteractionEmitter _emitter;
        [SerializeField] private ConditionMode _mode = ConditionMode.And;

        private bool _emitting = false;

        private void Start() {
            if (!_emitter) {
                _emitter = GetComponent<ConditionalEmitter>();
            }
        }

        public override void AcceptInteraction(InteractionSource source) {
            if (!IsValidInteraction(source)) {
                return;
            }
            _currentSignals = Mathf.Min(_maxSignals, _currentSignals + 1);
            CheckStateChange(source);
        }

        public override void CancelInteraction(InteractionSource source) {
            if (!IsValidInteraction(source)) {
                return;
            }
            _currentSignals = Mathf.Max(0, _currentSignals - 1);
            CheckStateChange(source);
        }

        private void CheckStateChange(InteractionSource source) {
            if (!_emitting && IsValid()) {
                _emitting = true;
                _emitter.StartInteract(source);
                return;
            }
            if (_emitting && !IsValid()) {
                _emitting = false;
                _emitter.FinishInteract(source);
                return;
            }
        }

        private bool IsValid() {
            return _mode switch {
                ConditionMode.And => _currentSignals == _maxSignals,
                ConditionMode.Xor => _currentSignals == 1,
                _ => _currentSignals > 0
            };
        }
    }
}