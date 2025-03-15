using System;

using UnityEngine;

namespace Interactable {
    [System.Flags] public enum InteractionSource { None = 0, Lever = 2 << 1, Button = 2 << 2, PressurePlate = 2 << 3, Conditional = 2 << 4 }
    public abstract class InteractionEmitter : MonoBehaviour {
        [SerializeField] protected InteractionSource _type;
        [SerializeField] protected InteractionReceiver[] _receivers;

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

        public virtual void PlayerTrigger() {
            StartInteract(_type);
        }
    }

}