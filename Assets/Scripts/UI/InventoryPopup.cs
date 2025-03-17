using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Items;

namespace UI {
    [RequireComponent(typeof(WorldSpaceUI))]
    public class InventoryPopup : MonoBehaviour {
        private Image _image;
        private TMP_Text _text;

        private void Start() {
            GetReferences();
        }

        private void GetReferences() {
            _image = GetComponentsInChildren<Image>().First(image => image.transform != transform);
            _text = GetComponentInChildren<TMP_Text>();
        }

        public void Init(ItemData data, int count) {
            if (!_image || _text) {
                GetReferences();
            }
            _image.sprite = data.Sprite;
            _text.text = count.ToString();
        }

        public void UpdateState(bool hasRequired) {
            _image.color = hasRequired ? Color.green : Color.red;
        }
    }
}