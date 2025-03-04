using System;
using UnityEngine;

public class HurtEventArgs : EventArgs {
    public Transform transformSource { get; private set; }

    public HurtEventArgs(Transform transformSource) {
        this.transformSource = transformSource;
    }

    public static HurtEventArgs Of(Transform transformSource) {
        return new HurtEventArgs(transformSource);
    }
}