using UnityEngine;

using UI;

namespace Interactable {
    [System.Flags] public enum InteractionSource { None = 0, Lever = 2 << 1, Button = 2 << 2, PressurePlate = 2 << 3, Conditional = 2 << 4, Inventory = 2 << 5, Inverter = 2 << 6 }

    public abstract class InteractionEmitter : MonoBehaviour {
        [SerializeField] protected InteractionSource _type;
        [SerializeField] protected InteractionReceiver[] _receivers;
        [SerializeField] protected InteractablePopup _popup = null;


        protected virtual void Awake() {
            CreatePopup();
        }

        public virtual void StartInteract(InteractionSource source) {
            foreach (InteractionReceiver receiver in _receivers) {
                if (receiver.IsValidInteraction(_type)) {
                    receiver.AcceptInteraction(_type);
                }
            }
        }

        public virtual void FinishInteract(InteractionSource source) {
            foreach (InteractionReceiver receiver in _receivers) {
                if (receiver.IsValidInteraction(_type)) {
                    receiver.CancelInteraction(_type);
                }
            }
        }

        protected virtual void CreatePopup() {
            _popup = Instantiate(AssetServer.Instance.InteractablePopup, UIManager.Instance.WorldCanvas).GetComponent<InteractablePopup>();
            _popup.transform.SetPositionAndRotation(transform.position + transform.forward * 0.5f, transform.rotation);
        }

        public virtual void PlayerTrigger() {
            StartInteract(_type);
        }

        public void MouseOver() {
            _popup.Show();
        }

        public void MouseExit() {
            _popup.Hide();
        }
    }

}