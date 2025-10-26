using UnityEngine;

public class Flipper : Organ {

    [Header("Flipper Settings")]
    public float thrustForce = 1f;

	public override void Action() {
		Vector3 forceDirection = transform.forward;
		player.rb.AddForceAtPosition(thrustForce * value * forceDirection, transform.position);
	}
	
		void Update()
	{
		transform.Rotate(Vector3.forward, 1000f * Time.deltaTime * value, Space.Self);
	}

}
