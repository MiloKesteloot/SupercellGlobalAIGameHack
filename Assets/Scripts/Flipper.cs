using UnityEngine;

public class Flipper : Organ {

    [Header("Flipper Settings")]
    public float thrustForce = 1f;

    public override void Action() {
        Vector3 forceDirection = transform.forward;
        Debug.Log(value);
        player.rb.AddForceAtPosition(thrustForce * value * forceDirection, transform.position);
    }
}
