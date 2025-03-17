using UnityEngine;

using Utilities;

namespace UI {
    public class WorldSpaceUI : MonoBehaviour {
        private void LateUpdate() {
            Transform camera = Helpers.Instance.MainCamera.transform;
            transform.LookAt(transform.position + camera.forward, camera.up);
        }
    }
}