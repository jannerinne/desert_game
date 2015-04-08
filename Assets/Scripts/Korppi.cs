using UnityEngine;
using System.Collections;

public class Korppi : MonoBehaviour {

	public bool fly = false;

	public Vector2 flyDir = new Vector2(4.0f, 3.0f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!fly && Mathf.Abs(transform.position.x) < 3.0f) {
			Vector3 lScale = transform.localScale;
			lScale.x = Mathf.Abs(lScale.x);
			transform.localScale = lScale;
			fly = true;
			GetComponent<Animator>().enabled = true;
			transform.Translate(0f, 0.5f, 0f);

			var flap = GetComponent<AudioSource>();
			if (flap != null) {
				flap.Play();
			}
		}

		if (fly) {
			transform.Translate(flyDir.x * Time.deltaTime, flyDir.y * Time.deltaTime, 0f);
		}
	}
}
