using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ValueWrapper<T> {
    [SerializeField] private T value;

    public ValueWrapper(T value) {
        this.value = value;
    }

    public T Value() {
        return value;
    }
}