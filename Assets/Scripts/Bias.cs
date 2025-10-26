using UnityEngine;

public class Bias : Organ {
    [Header("Bias Settings")]
    public float bias = 1;

    public override float ExternalSignal() {
        return bias;
    }
}

