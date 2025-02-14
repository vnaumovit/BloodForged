using System;
using UnityEngine;

public class HintEventArgs : EventArgs {
    public Transform transformSource { get; private set; }

    public HintEventArgs(Transform transformSource) {
        this.transformSource = transformSource;
    }

    public static HintEventArgs Of(Transform transformSource) {
        return new HintEventArgs(transformSource);
    }
}