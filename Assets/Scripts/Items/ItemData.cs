using UnityEngine;

namespace Items {
    public enum ItemType { Key, SmallKey, SpecialKey }
    [CreateAssetMenu(menuName = "Item")]
    public class ItemData : ScriptableObject {
        public string Name;
        public int MaxCount;
        public Sprite Sprite;
        public ItemType Type;
    }
}