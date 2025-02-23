using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> {
    [SerializeField] private List<TKey> keys;
    [SerializeField] private List<ValueWrapper<TValue>> values;
    private Dictionary<TKey, ValueWrapper<TValue>> dictionary;

    public Dictionary<TKey, ValueWrapper<TValue>> GetDictionary() {
        if (dictionary != null) return dictionary;
        var d = new Dictionary<TKey, ValueWrapper<TValue>>();
        for (int i = 0; i < keys.Count; i++) {
            d[keys[i]] = values[i];
        }
        dictionary = d;
        return d;
    }

    public void FromDictionary(Dictionary<TKey, ValueWrapper<TValue>> d) {
        keys = new List<TKey>(d.Keys);
        values = new List<ValueWrapper<TValue>>(d.Values);
    }

    public TValue Value(TKey key) {
        return GetDictionary()[key].Value();
    }
}