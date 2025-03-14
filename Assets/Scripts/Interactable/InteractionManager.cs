using Cinemachine;

using UnityEngine;

using Utilities;

namespace Interactable {
    public class InterationManager : Singleton<InterationManager> {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _distance = 10.0f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private InteractionEmitter _emitter;

        private RaycastHit _hit;

        public InteractionEmitter CurrentEmitter => _emitter;

        private void Start() {
            _mask = 1 << LayerMask.NameToLayer("InteractionEmitter");
            if (!_camera) {
                _camera = FindFirstObjectByType<CinemachineVirtualCamera>().transform;
            }
        }

        private void FixedUpdate() {
            float distance = float.MaxValue;
            Ray ray = Helpers.Instance.MainCamera.ViewportPointToRay(Vector2.one * 0.5f);
            _emitter = null;
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _distance, _mask))
                if (hit.distance < distance) {
                    _emitter = hit.collider.GetComponent<InteractionEmitter>();
                }
        }

        private void Update() {
            if (_emitter && PlayerInputs.Instance.Interaction) {
                _emitter.PlayerTrigger();
            }
        }
    }

}