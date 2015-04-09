using UnityEngine;
using System.Collections;

public class BottleThrow : MonoBehaviour {

	public Vector2 velocity;
	
	// Update is called once per frame
	void Update () {
		velocity = velocity + new Vector2(0f, -Time.deltaTime * 8f);
		transform.Rotate(Vector3.forward, 400f * Time.deltaTime);
		transform.Translate(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0f, Space.World);

		if (transform.position.y < -6f) {
			Destroy(gameObject);
		}
	}
}
