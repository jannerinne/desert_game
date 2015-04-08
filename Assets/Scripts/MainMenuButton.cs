using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour {

	public int buttonIndex = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		if (buttonIndex == 0) {
			Application.LoadLevel(1);
		}
		else if (buttonIndex == 1) {
			Application.Quit();
		}
	}
}
