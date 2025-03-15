using UnityEngine;

namespace Interactable {
    public class ConditionalReceiver : InteractionReceiver {
        [SerializeField] private int _requiredSignals = 2;
        [SerializeField] private int _currentSignals = 0;
        [SerializeField] private InteractionEmitter _emitter;

        public override void AcceptInteraction(InteractionSource source) {
            if (!IsValidInteraction(source)) {
                return;
            }
            _currentSignals = Mathf.Min(_requiredSignals, _currentSignals + 1);
            if (_currentSignals >= _requiredSignals) {
                _emitter.StartInteract(InteractionSource.Conditional);
            }
        }

        public override void CancelInteraction(InteractionSource source) {
            if (!IsValidInteraction(source)) {
                return;
            }
            _currentSignals = Mathf.Max(0, _currentSignals - 1);
            if (_currentSignals == _requiredSignals - 1) {
                _emitter.FinishInteract(InteractionSource.Conditional);
            }
        }
    }
}