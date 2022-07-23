using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar : MonoBehaviour {

	public float AntiRoll = 5000.0f;

	private WheelCollider WheelL;
	private WheelCollider WheelR;
	private Rigidbody car;

	void Start(){
		WheelL = GetComponent<CarController>().CLeftFront;
		WheelR = GetComponent<CarController>().CRightFront;
		car = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		WheelHit hit;
		float travelL = 1.0f;
		float travelR = 1.0f;


		bool groundedL = WheelL.GetGroundHit (out hit);
		if (groundedL) {
			travelL = (-WheelL.transform.InverseTransformPoint (hit.point).y - WheelL.radius) / WheelL.suspensionDistance;
		}

		bool groundedR = WheelR.GetGroundHit (out hit);
		if (groundedR) {
			travelR = (-WheelR.transform.InverseTransformPoint (hit.point).y - WheelR.radius) / WheelR.suspensionDistance;
		}

		float antiRollForce = (travelL - travelR) * AntiRoll;

		if (groundedL)
			car.AddForceAtPosition (WheelL.transform.up * -antiRollForce, WheelL.transform.position);

		if (groundedR)
			car.AddForceAtPosition (WheelR.transform.up * antiRollForce, WheelR.transform.position);
	}
}
