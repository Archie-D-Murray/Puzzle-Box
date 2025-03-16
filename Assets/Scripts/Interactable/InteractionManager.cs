using System.Collections.Generic;

using Cinemachine;

using UnityEngine;
using UnityEngine.InputSystem;

using Utilities;

namespace Interactable {
    public class InterationManager : Singleton<InterationManager> {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _distance = 10.0f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private InteractionEmitter _emitter;

        private RaycastHit _hit;
        private HashSet<InteractionEmitter> _hovered = new HashSet<InteractionEmitter>();

        public InteractionEmitter CurrentEmitter => _emitter;

        private void Start() {
            _mask = 1 << LayerMask.NameToLayer("InteractionEmitter");
            if (!_camera) {
                _camera = FindFirstObjectByType<CinemachineVirtualCamera>().transform;
            }
            PlayerInputs.Instance.Actions.Player.Interaction.started += OnInteraction;
        }

        private void FixedUpdate() {
            float distance = float.MaxValue;
            Ray ray = Helpers.Instance.MainCamera.ViewportPointToRay(Vector2.one * 0.5f);
            InteractionEmitter prev = _emitter;
            _emitter = null;
            foreach (RaycastHit hit in Physics.RaycastAll(ray, _distance, _mask)) {
                if (hit.distance < distance) {
                    _emitter = hit.collider.GetComponent<InteractionEmitter>();
                }
            }
            if (_emitter) {
                _hovered.Add(_emitter);
                _emitter.MouseOver();
            }
            if (_hovered.Count > 0) {
                foreach (InteractionEmitter emitter in _hovered) {
                    if (emitter == _emitter) {
                        continue;
                    }
                    emitter.MouseExit();
                }
                _hovered.RemoveWhere(emitter => emitter != _emitter);
            }
        }

        private void OnInteraction(InputAction.CallbackContext context) {
            if (_emitter) {
                _emitter.PlayerTrigger();
            }
        }
    }

}