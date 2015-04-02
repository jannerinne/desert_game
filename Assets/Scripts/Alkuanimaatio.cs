using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Alkuanimaatio : MonoBehaviour {

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;

	private List<string> dialogue = new List<string>(); // Menossa oleva dialogi.

	private GUIStyle style;

	public Font font;

	void Start () {
		style = new GUIStyle();
		style.wordWrap = true;
		style.fontSize = 20;
		style.normal.textColor = Color.white;
		style.alignment = TextAnchor.MiddleCenter;
		style.font = font;

		dialogue.Add("Can you find out in which direction the safetown is?");
		dialogue.Add("Soon. This sandstorm is disrupting the signal.");
		dialogue.Add("I hope it'll be found quickly... We don't have much food or water left.");
		dialogue.Add("#PowerOff");
		/*
		dialogue.Add("NO!");
		dialogue.Add("Get out fast!");
		dialogue.Add("<outside>");
		dialogue.Add("What are we going to do now...?");
		dialogue.Add("I suggest we go that way, we have to come by some kind of shelter or something.");
		*/
	}

	void Update () {

		if (dialogue.Count > 0 && dialogue[0].StartsWith("#")) {
			string command = dialogue[0].Substring(1);
			dialogue.RemoveAt(0);
			Invoke(command, 0f);
		}
		
		if (dialogue.Count > 0 && Input.anyKeyDown) {
			dialogue.RemoveAt(0);
		}

	}

	void OnGUI() {
		if (dialogue.Count > 0 && !dialogue[0].StartsWith("#")) {
			var w = Screen.width;
			var h = Screen.height;
			var rect = new Rect(w * 0.333f, h * 0.1f, w * 0.333f, h * 0.3f);
			GUI.Box(rect, ""); // todo: lisää kuva
			GUI.Box(rect, dialogue[0], style);
		}
	}

	void SetSprite(Sprite s) {
		GetComponent<SpriteRenderer>().sprite = s;
	}

	// -----------------------------------------------------------

	void PowerOff() {
		for(int i=0; i<10; i++) {
			if (i % 2 == 0) {
				Invoke("PowerSprite1", i * 0.1f);
			}
			else {
				Invoke("PowerSprite2", i * 0.1f);
			}
		}
		Invoke("PowerOff2", 2f);
	}

	void PowerOff2() {
		dialogue.Add("The power!");
		dialogue.Add("That must be because of the storm.");
		dialogue.Add("There is something wrong at the connection mast!");
		dialogue.Add("I will go check it out.");
		dialogue.Add("Be careful!");
		dialogue.Add("#Climb");
	}

	void PowerSprite1() {
		SetSprite(sprite1);
	}

	void PowerSprite2() {
		SetSprite(sprite2);
	}

	// -----------------------------------------------------------

	void Climb() {
		dialogue.Add("NO!");
	}
}
