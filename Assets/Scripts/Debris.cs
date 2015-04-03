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
			speed += Time.deltaTime * 0.5f;
			transform.Translate(0f, -speed, 0f);

			for (int i=0; i < transform.childCount; i++) {
				Transform deb = transform.GetChild(i);
				if (deb.gameObject.name.StartsWith("falling")) {
					deb.Rotate(Vector3.forward, Random.Range(300f, 800f) * Time.deltaTime);
				}
			}
		}
	}

	public void Fall() {
		falling = true;
	}
}
