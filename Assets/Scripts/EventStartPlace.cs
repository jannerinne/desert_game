using UnityEngine;
using System.Collections;

public class EventStartPlace : MonoBehaviour {

	public Vector2 StartOffset;

	// Use this for initialization
	void Start () {
		float x = transform.position.x + StartOffset.x;
		float y = transform.position.y + StartOffset.y;
		transform.position = new Vector3(x, y, transform.position.z);
	}
}
