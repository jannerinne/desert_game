using UnityEngine;
using System.Collections;

public class IntroCharacter : MonoBehaviour {

	private int running = 0;

	public void Run(int dir) {
		running = dir;
		GetComponent<Animator>().SetBool("run", true);
		Vector3 localScale = transform.localScale;
		localScale.x = Mathf.Abs(localScale.x) * Mathf.Sign(running);
		transform.localScale = localScale;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (running != 0) {
			transform.Translate(2f * running * Time.deltaTime, 0f, 0f);
		}
	}
}
