using UnityEngine;
using System.Collections;

public class SimpleTextEvent : MonoBehaviour {

	public string[] eventDialogText;

	private GameManagerC manager;
	public bool used = false;
	private GUIStyle style;

	void Start () {
		manager = GameObject.Find("GameManager").GetComponent<GameManagerC>();
		style = new GUIStyle();
		style.wordWrap = true;
		style.fontSize = 20;
		style.normal.textColor = Color.white;
		style.alignment = TextAnchor.MiddleCenter;
		style.font = manager.font;
	}

	bool PlayerIsNear() {
		return Mathf.Abs(transform.position.x) < 1.0;
	}

	void OnGUI() {
		if(PlayerIsNear() && !used) {
			var w = Screen.width;
			var h = Screen.height;
			var rect = new Rect(w * 0.4f, h * 0.1f, w * 0.2f, h * 0.1f);
			GUI.Box(rect, "");
			GUI.Box(rect, "Press Space", style);
		}
	}

	void Update () {
		if (!used && manager.PlayerCanAct() && Input.GetKeyDown(KeyCode.Space) && PlayerIsNear()) {
			used = true;
			foreach (string line in eventDialogText) {
				//manager.dialogue.Add(line);
				manager.AddDialog(line);
			}
			//Destroy(gameObject);
		}
	}
}
