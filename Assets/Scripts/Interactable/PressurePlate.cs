using System.Collections.Generic;

using UnityEngine;

namespace Interactable {
    public class PressurePlate : InteractionEmitter {
        [SerializeField] private int _count = 1;
        private HashSet<GameObject> _objects = new HashSet<GameObject>();
        [SerializeField] private Vector3 _pressed;
        [SerializeField] private Vector3 _normal;
        [SerializeField] private bool _isPressed = false;

        private void Start() {
            _pressed = transform.position;
            _pressed.y -= transform.localScale.y;
            _normal = transform.position;
        }
        private void FixedUpdate() {
            transform.position = Vector3.MoveTowards(transform.position, _isPressed ? _pressed : _normal, Time.fixedDeltaTime);
        }

        protected override void CreatePopup() {
            return;
        }

        public override void PlayerTrigger() {
            return;
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.HasComponent<Tags.PressurePlateActivator>()) {
                _objects.Add(collider.gameObject);
                if (_objects.Count >= _count) {
                    StartInteract(_type);
                    _isPressed = true;
                }
            }
        }

        private void OnTriggerExit(Collider collider) {
            if (collider.gameObject.HasComponent<Tags.PressurePlateActivator>()) {
                _objects.Remove(collider.gameObject);
                if (_objects.Count == _count - 1) {
                    FinishInteract(_type);
                    _isPressed = false;
                }
            }
        }
    }
}