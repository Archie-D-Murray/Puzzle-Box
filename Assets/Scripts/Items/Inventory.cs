using System.Collections.Generic;

using UnityEngine;

namespace Items {
    public class Inventory : MonoBehaviour {
        [SerializeField] private List<Item> _items = new List<Item>(1);
        private Dictionary<ItemType, int> _lookup = new Dictionary<ItemType, int>();

        private void Start() {
            for (int i = 0; i < _items.Count; i++) {
                _lookup.Add(_items[i].Data.Type, i);
            }
        }

        public void AddItem(ItemData data, int count = 1) {
            if (_lookup.TryGetValue(data.Type, out int index)) {
                _items[index].Count = Mathf.Min(_items[index].Count + 1, _items[index].Data.MaxCount);
            } else {
                int i = 0;
                while (i < _items.Count) {
                    if (_items[i] == null) {
                        _items[i] = new Item();
                        _items[i].Data = data;
                        _items[i].Count = Mathf.Min(count, data.MaxCount);
                        _lookup.Add(data.Type, i);
                    } else {
                        i++;
                    }
                }
                Item toAdd = new Item();
                toAdd.Data = data;
                toAdd.Count = Mathf.Min(count, data.MaxCount);
                _lookup.Add(data.Type, _items.Count);
                _items.Add(toAdd);
            }
        }

        public void RemoveItem(ItemData data, int count = 1) {
            if (_lookup.TryGetValue(data.Type, out int index)) {
                if (count >= _items[index].Count) {
                    _items.RemoveAt(index);
                    _lookup.Remove(data.Type);
                } else {
                    _items[index].Count -= index;
                }
            } else {
                Debug.LogWarning($"Tried to remove non-present item: {data.Name}", this);
            }
        }

        public bool ContainsItem(ItemData data) {
            return _lookup.ContainsKey(data.Type);
        }

        public int CountItem(ItemData data) {
            if (!ContainsItem(data)) { return -1; }
            return _items[_lookup[data.Type]].Count;
        }
    }
}