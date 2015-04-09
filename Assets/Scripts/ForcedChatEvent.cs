using UnityEngine;
using System.Collections;

public class ForcedChatEvent : MonoBehaviour {

	public string[] eventDialogText;
	
	private GameManagerC manager;

	private Vector3 playerScale;
	private Vector3 friendScale;
	
	void Start () {
		manager = GameObject.Find("GameManager").GetComponent<GameManagerC>();
		foreach (string line in eventDialogText) {
			manager.AddDialog(line);
		}

		if (manager.playerDir > 0) {
			var lScale = manager.player.transform.localScale;
			lScale.x *= -1;
			playerScale = lScale;
			friendScale = manager.friend.transform.localScale;
		}
		else {
			var lScale = manager.friend.transform.localScale;
			lScale.x *= -1;
			friendScale = lScale;
			playerScale = manager.player.transform.localScale;
		}
	}

	void Update () {
		if (!manager.PlayerCanAct()) {
			manager.player.transform.localScale = playerScale;
			manager.friend.transform.localScale = friendScale;
		}
	}
}
