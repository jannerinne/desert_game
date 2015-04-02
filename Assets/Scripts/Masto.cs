using UnityEngine;
using System.Collections;

public class Masto : MonoBehaviour {

	public float rotation = 0f;
	public float swingAmplitude = 1f;
	public float swingSpeed = 1f;
	public float fallSpeed = 0f;

	public bool falling = false;

	// Use this for initialization
	void Start () {
	
	}

	public void Fall() {
		falling = true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.AngleAxis(rotation, Vector3.forward);

		if (falling) {
			fallSpeed += Time.deltaTime;
			rotation -= fallSpeed * fallSpeed;
		}
		else {
			rotation = Mathf.Sin(Time.time * swingSpeed) * swingAmplitude;
		}
	}
}
