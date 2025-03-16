using UnityEngine;

using Utilities;

namespace UI {
    public class InteractablePopup : MonoBehaviour {
        private CanvasGroup _canvas;
        private float _targetAlpha = 0.0f;

        private void Start() {
            _canvas = GetComponent<CanvasGroup>();
        }

        public void Show() {
            if (_canvas.alpha != 0.0f) { return; }
            _targetAlpha = 1.0f;
        }

        public void Hide() {
            if (_canvas.alpha != 1.0f) { return; }
            _targetAlpha = 0.0f;
        }

        private void FixedUpdate() {
            _canvas.alpha = Mathf.MoveTowards(_canvas.alpha, _targetAlpha, 2.0f * Time.fixedDeltaTime);
        }

        private void LateUpdate() {
            Transform camera = Helpers.Instance.MainCamera.transform;
            transform.LookAt(transform.position + camera.forward, camera.up);
        }
    }
}