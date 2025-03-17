using UnityEngine;

using Items;
using UI;

namespace Interactable {
    public class InventoryEmitter : InteractionEmitter {
        [SerializeField] private ItemData _data;
        [SerializeField] private int _consumeCount = 1;
        [SerializeField] private bool _unlocked = false;
        [SerializeField] private InventoryPopup _inventoryPopup;

        private Inventory _inventory;

        private void Start() {
            _inventoryPopup = Instantiate(AssetServer.Instance.InventoryPopup, UIManager.Instance.WorldCanvas).GetComponent<InventoryPopup>();
            _inventoryPopup.transform.position = transform.position + 1.0f * transform.up + transform.forward;
            _inventoryPopup.gameObject.SetActive(false);
        }

        public override void PlayerTrigger() {
            if (!_unlocked && _inventory && HasRequired()) {
                StartInteract(_type);
                _unlocked = true;
                if (_consumeCount > 0) {
                    _inventory.RemoveItem(_data, _consumeCount);
                    _popup.gameObject.SetActive(false);
                    _inventoryPopup.gameObject.SetActive(false);
                }
            } else if (!_unlocked && _inventory) {
                Debug.Log($"Player has {_inventory.CountItem(_data)}/{_consumeCount} of {_data.Name}");
            }
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.TryGetComponent(out Inventory inventory)) {
                if (!_inventoryPopup.gameObject.activeSelf && !_unlocked) {
                    _inventoryPopup.gameObject.SetActive(true);
                }
                _inventory = inventory;
                _inventoryPopup.Init(_data, _consumeCount);
                _inventoryPopup.UpdateState(HasRequired());
            }
        }

        private void FixedUpdate() {
            if (_inventory && !_unlocked) {
                _inventoryPopup.UpdateState(HasRequired());
            }
        }

        private void OnTriggerExit(Collider collider) {
            if (collider.gameObject.HasComponent<Inventory>()) {
                _inventory = null;
            }
        }

        private bool HasRequired() {
            return _inventory.CountItem(_data) >= _consumeCount;
        }
    }
}