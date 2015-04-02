using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		var start = new Rect(50, Screen.height * 0.3f, 300, 120); // Start painikkeen koko ja paikka.
		var quit = new Rect(50, Screen.height * 0.6f, 300, 120); // Quit painikkeen koko ja paikka.

		if (GUI.Button(start, "Start a new game")) {
			Application.LoadLevel(1);
		}

		if (GUI.Button(quit, "Quit")) {
			Application.Quit();
		}
	}
}
