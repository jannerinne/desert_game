using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour {

	public bool falling = false;

	public Transform target;

	private float speed = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (falling && transform.position.y > target.position.y) {
			speed += Time.deltaTime * 0.7f;
			transform.Translate(0f, -speed, 0f);
		}
	}

	public void Fall() {
		falling = true;
	}
}
