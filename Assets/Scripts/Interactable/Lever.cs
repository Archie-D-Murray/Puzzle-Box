using UnityEngine;

namespace Interactable {
    public class Lever : InteractionEmitter {
        [SerializeField] private bool _on = false;

        public override void PlayerTrigger() {
            _on = !_on;
            if (_on) {
                StartInteract(_type);
            } else {
                FinishInteract(_type);
            }
        }
    }
}