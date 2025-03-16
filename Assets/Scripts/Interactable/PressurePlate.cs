using System.Collections.Generic;

using UnityEngine;

namespace Interactable {
    public class PressurePlate : InteractionEmitter {
        [SerializeField] private int _count = 1;
        private HashSet<GameObject> _objects = new HashSet<GameObject>();

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
                }
            }
        }

        private void OnTriggerExit(Collider collider) {
            if (collider.gameObject.HasComponent<Tags.PressurePlateActivator>()) {
                _objects.Remove(collider.gameObject);
                if (_objects.Count == _count - 1) {
                    FinishInteract(_type);
                }
            }
        }
    }
}