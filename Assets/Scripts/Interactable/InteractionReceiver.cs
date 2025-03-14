using UnityEngine;

namespace Interactable {
    public abstract class InteractionReceiver : MonoBehaviour {
        [SerializeField] protected InteractionSource _sources;
        public abstract void AcceptInteraction(InteractionSource source);
        public abstract void CancelInteraction(InteractionSource source);

        public bool IsValidInteraction(InteractionSource source) {
            return (_sources & source) == source;
        }
    }

}