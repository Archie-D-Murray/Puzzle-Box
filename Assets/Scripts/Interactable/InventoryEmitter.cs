using UnityEngine;

using Items;

namespace Interactable {
    public class InventoryEmitter : InteractionEmitter {
        [SerializeField] private ItemData _data;
        [SerializeField] private int _consumeCount = 1;

        private Inventory _inventory;

        public override void PlayerTrigger() {
            if (_inventory && _inventory.CountItem(_data) >= _consumeCount) {
                StartInteract(_type);
                if (_consumeCount > 0) {
                    _inventory.RemoveItem(_data, _consumeCount);
                }
            } else if (_inventory) {
                Debug.Log($"Tried to open lock without key: {_data.Name} in {_inventory.gameObject.name}!");
            }
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.TryGetComponent(out Inventory inventory)) {
                _inventory = inventory;
            }
        }

        private void OnTriggerExit(Collider collider) {
            if (collider.gameObject.HasComponent<Inventory>()) {
                _inventory = null;
            }
        }

    }
}