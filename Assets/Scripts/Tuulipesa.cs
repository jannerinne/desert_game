using UnityEngine;
using System.Collections;

public class Tuulipesa : MonoBehaviour {

	private float startY = 0f;

	public float speed = 3f;

	// Use this for initialization
	void Start () {
		startY = transform.position.y;
		transform.position = new Vector3(12f, startY, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-Time.deltaTime * speed, 0f, 0f, Space.World);
		transform.Rotate(Vector3.forward, Time.deltaTime * 400f);
		//transform.localRotation = Quaternion.AngleAxis(Time.time * 200f, Vector3.forward);

		float newY = startY + Mathf.Abs(0.8f * Mathf.Sin(Time.time * 3f));
		transform.position = new Vector3(transform.position.x, newY, transform.position.z);
	}
}
