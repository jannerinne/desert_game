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
	public GameObject tyrone;

	private List<string> dialogue = new List<string>(); // Menossa oleva dialogi.

	private float fadeSpeed = 1.2f;

	private float fade = 1f;
	private float fadeTarget = 0f;
	private string fadeInvoke = "StartDialog";
	private bool fading = true;

	private GUIStyle style;
	public Font font;

	void Awake() {
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

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
	}

	void StartDialog() {
		dialogue.Add("Shaun: Can you find out how we can get to Safetown?");
		dialogue.Add("Darin: Soon. This sandstorm is messing with the signal.");
		dialogue.Add("Monique: I hope we find it quickly...We don't have much foor or water left...");
		dialogue.Add("Shaun: God Damn It! Most of our supplies were in the last safehouse we were holed up in, but damn bandits overran the whole area.");
		dialogue.Add("Monique: I wouldn't have made it if it wasn't for Darin. I haven't even thanked you properly. Thank you so much for saving my life.");
		dialogue.Add("Darin: Oh that...it was nothing.");
		dialogue.Add("#Waiting");
	}

	void Update () {
		if (fading) {
			fade += (fadeTarget - fade) * fadeSpeed * Time.deltaTime;
			if (Mathf.Abs(fade - fadeTarget) < 0.1f) {
				fading = false;
				if (fadeInvoke != null) {
					Invoke(fadeInvoke, 0f);
				}
			}
			guiTexture.color = Color.Lerp(Color.clear, Color.white, fade);
			return;
		}

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

	private void FadeTo(float target, string invoke) {
		fading = true;
		fadeTarget = target;
		fadeInvoke = invoke;
	}

	// ----------------------------------------------------------

	void Waiting() {
		Invoke("PowerOff", 2f);
	}

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
		dialogue.Add("Darin: The power is failing!");
		dialogue.Add("Tyrone: Must be due to the storm.");
		dialogue.Add("Shaun: There must be something wrong with the satellite tower!");
		dialogue.Add("Tyrone: I'll go check it out.");
		dialogue.Add("Monique: Please be careful!");
		dialogue.Add("#RunRight");
	}

	void RunRight() {
		tyrone.GetComponent<IntroCharacter>().Run(2);
		Invoke("ClimbFadeOut", 3f);
	}

	void ClimbFadeOut() {
		FadeTo(1f, "Climb");
	}

	void PowerSprite1() {
		SetSprite(sprite1);
	}

	void PowerSprite2() {
		SetSprite(sprite2);
	}

	// -----------------------------------------------------------

	void Climb() {
		// Katto näkymä.
		SetSprite(sprite3);

		FadeTo(0f, null);

		// Piilotetaan hahmot.
		Destroy(tyrone);
		woman.transform.position = new Vector3(0f, -16f, 0f);
		player.transform.position = new Vector3(0f, -16f, 0f);
		friend.transform.position = new Vector3(0f, -16f, 0f);
		tyrone.transform.position = new Vector3(0f, -16f, 0f);

		masto.transform.position = new Vector3(-6.68f, -6.94f, 0f);
		Invoke("RoofDialog", 2f);
	}

	void RoofDialog() {
		masto.GetComponent<AudioSource>().Play();
		dialogue.Add("Tyrone: Everything's fucked...How can a storm do thi...");
		dialogue.Add("Tyrone: ...");
		dialogue.Add("Tyrone: Shit, the tower is... it's going to fall over in this storm...");
		dialogue.Add("#Fall");
		dialogue.Add("Tyrone: AAAAHHHHHHH!!!");
	}

	void Fall() {
		masto.GetComponent<Masto>().Fall();
		Invoke("FallFade", 1f);
	}

	void FallFade() {
		FadeTo(1f, "BackInside");
	}

	void BackInside() {
		masto.GetComponent<AudioSource>().Stop();
		FadeTo(0f, null);
		dialogue.Clear();
		SetSprite(sprite2);
		woman.transform.position = womanS;
		player.transform.position = playerS;
		friend.transform.position = friendS;
		Destroy(masto);
		debris.GetComponent<Debris>().Fall();
		Invoke("DebrisFallen", 2f);
	}

	void DebrisFallen() {
		dialogue.Add("Darin: MONIQUE!");
		dialogue.Add("Shaun: We have to get out!");
		dialogue.Add("Darin: We can't just leave her! She might still be alive!");
		dialogue.Add("Shaun: THERE'S NOTHING WE CAN DO!");
		dialogue.Add("#RunLeft");
	}

	void RunLeft() {
		player.GetComponent<IntroCharacter>().Run(-2);
		Invoke("LateRunLeft", 0.5f);
		Invoke("OutsideFade", 1.5f);
	}

	void LateRunLeft() {
		friend.GetComponent<IntroCharacter>().Run(-2);
	}

	void OutsideFade() {
		FadeTo(1f, "Outside");
	}

	// -----------------------------------------------------------

	void Outside() {
		var pl = GameObject.Find("BackgroundMusic");
		if (pl != null) {
			var music = pl.GetComponent<BgMusic>();
			music.PlayGameMusic();
			music.PlayWind();
		}
		Application.LoadLevel(2);
	}
}
