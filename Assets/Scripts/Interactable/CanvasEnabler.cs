using UnityEngine;

namespace Interactable {
    public class CanvasEnabler : InteractionReceiver {
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private float _target = 0.0f;

        private void Start() {
            if (!_canvas) {
                _canvas = GetComponent<CanvasGroup>();
            }
            _canvas.alpha = _target;
        }

        public override void AcceptInteraction(InteractionSource source) {
            _target = 1.0f;
        }

        public override void CancelInteraction(InteractionSource source) {
            _target = 0.0f;
        }

        private void FixedUpdate() {
            _canvas.alpha = Mathf.MoveTowards(_canvas.alpha, _target, 2.0f * Time.fixedDeltaTime);
        }
    }
}