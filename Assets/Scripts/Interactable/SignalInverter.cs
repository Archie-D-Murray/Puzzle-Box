using UnityEngine;

namespace Interactable {
    [RequireComponent(typeof(ConditionalEmitter))]
    public class SignalInverter : InteractionReceiver {
        [SerializeField] private InteractionEmitter _emitter;
        [SerializeField] private bool _startOn = false;
        [SerializeField] private InteractionSource _type;

        private void Start() {
            _emitter = GetComponent<InteractionEmitter>();
            if (_startOn) {
                _emitter.StartInteract(_type);
            }
        }

        public override void AcceptInteraction(InteractionSource source) {
            _emitter.FinishInteract(source);
        }

        public override void CancelInteraction(InteractionSource source) {
            _emitter.StartInteract(source);
        }
    }
}