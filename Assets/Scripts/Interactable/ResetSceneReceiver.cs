using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactable {
    public class ResetSceneReceiver : InteractionReceiver {
        public override void AcceptInteraction(InteractionSource source) {
            SceneManager.LoadScene(0);
        }

        public override void CancelInteraction(InteractionSource source) {
            return;
        }
    }
}