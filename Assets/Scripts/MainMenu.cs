using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Font font;
	private GUIStyle style;

	// Use this for initialization
	void Start () {
		style = new GUIStyle();
		style.wordWrap = true;
		style.fontSize = 20;
		style.normal.textColor = Color.white;
		style.alignment = TextAnchor.MiddleCenter;
		style.font = font;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		var start = new Rect(50, Screen.height * 0.3f, 300, 120); // Start painikkeen koko ja paikka.
		var quit = new Rect(50, Screen.height * 0.6f, 300, 120); // Quit painikkeen koko ja paikka.

		GUI.Box(start, "");
		if (GUI.Button(start, "Start a new game", style)) {
			Application.LoadLevel(1);
		}

		GUI.Box(quit, "");
		if (GUI.Button(quit, "Quit", style)) {
			Application.Quit();
		}
	}
}
