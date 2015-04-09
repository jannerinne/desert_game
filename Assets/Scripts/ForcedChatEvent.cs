using UnityEngine;
using System.Collections;

public class ForcedChatEvent : MonoBehaviour {

	public string[] eventDialogText;
	
	private GameManagerC manager;
	
	void Start () {
		manager = GameObject.Find("GameManager").GetComponent<GameManagerC>();
		foreach (string line in eventDialogText) {
			manager.AddDialog(line);
		}
	}
}
