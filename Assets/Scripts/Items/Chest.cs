using UnityEngine;
using UnityEngine.InputSystem;

namespace Items {
    public class Chest : MonoBehaviour {
        [SerializeField] private ItemData _data;
        [SerializeField] private int _count = 1;
        [SerializeField] private bool _looted = false;

        private Inventory _inventory;

        private void Start() {
            PlayerInputs.Instance.Actions.Player.Interaction.started += AddItem;
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

        private void AddItem(InputAction.CallbackContext context) {
            if (_inventory && !_looted) {
                _inventory.AddItem(_data, _count);
                transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
                _looted = true;
                enabled = false;
            }
        }
    }
}