using System;

namespace Items {
    [Serializable]
    public class Item {
        public ItemData Data;
        public int Count;

        public override int GetHashCode() {
            return HashCode.Combine(Count, Data.GetHashCode());
        }

        public override bool Equals(object obj) {
            if (obj is not Item) { return false; }
            Item item = obj as Item;
            return Data.name.Equals(item.Data.name);
        }
    }
}