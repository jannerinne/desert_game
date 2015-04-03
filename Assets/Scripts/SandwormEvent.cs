using UnityEngine;
using System.Collections;

public class SandwormEvent : MonoBehaviour {

	private GameManagerC manager;

	bool addedBoost = false;

	// Use this for initialization
	void Start () {
		transform.position = Vector3.zero;
		manager = GameObject.Find("GameManager").GetComponent<GameManagerC>();
		manager.dialogue.Add("Darin : Holy Shit! We gotta get out of here!");
	}
	
	void Update () {
		if (manager.PlayerCanAct()) {
			if (!addedBoost) {
				addedBoost = true;
				manager.walkSpeed *= 5;
				Invoke("EndBoost", 8f);
			}

			manager.walkSpeed += (manager.normalWalkSpeed - manager.walkSpeed) * 0.3f * Time.deltaTime;
		}
	}

	private void EndBoost() {
		manager.walkSpeed = manager.normalWalkSpeed;
		Destroy(gameObject);
	}
}
