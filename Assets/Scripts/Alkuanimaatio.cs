using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Alkuanimaatio : MonoBehaviour {

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;

	public GameObject masto;
	public GameObject debris;

	public GameObject player;
	private Vector3 playerS;
	public GameObject woman;
	private Vector3 womanS;
	public GameObject friend;
	private Vector3 friendS;

	private List<string> dialogue = new List<string>(); // Menossa oleva dialogi.

	private GUIStyle style;

	public Font font;

	void Start () {
		womanS = woman.transform.position;
		playerS = player.transform.position;
		friendS = friend.transform.position;

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
		SetSprite(sprite3);
		woman.transform.position = new Vector3(0f, -16f, 0f);
		player.transform.position = new Vector3(0f, -16f, 0f);
		friend.transform.position = new Vector3(0f, -16f, 0f);
		masto.transform.position = new Vector3(-6.68f, -6.94f, 0f);
		Invoke("Fall", 3f);
	}

	void Fall() {
		masto.GetComponent<Masto>().Fall();
		Invoke("BackInside", 2f);
	}

	void BackInside() {
		SetSprite(sprite2);
		woman.transform.position = womanS;
		player.transform.position = playerS;
		friend.transform.position = friendS;
		Destroy(masto);
		debris.GetComponent<Debris>().Fall();
		Invoke("DebrisFallen", 2f);
	}

	void DebrisFallen() {
		dialogue.Add("NO!");
		dialogue.Add("Get out fast!");
		dialogue.Add("#Outside");
	}

	// -----------------------------------------------------------

	void Outside() {
		Application.LoadLevel(2);
	}
}
