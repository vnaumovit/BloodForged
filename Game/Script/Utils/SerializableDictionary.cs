using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> {
    [SerializeField] private List<TKey> keys;
    [SerializeField] private List<TValue> values;
    private Dictionary<TKey, TValue> dictionary;

    public Dictionary<TKey, TValue> ToDictionary() {
        var d = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++) {
            d[keys[i]] = values[i];
        }

        return d;
    }

    public void FromDictionary(Dictionary<TKey, TValue> d) {
        keys = new List<TKey>(d.Keys);
        values = new List<TValue>(d.Values);
    }

    public TValue Value(TKey key) {
        dictionary ??= ToDictionary();
        return dictionary[key];
    }
}