using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerC : MonoBehaviour {	
	public GameObject player;
	public GameObject ground;
	public GameObject background;
	public GameObject background2;
	
	// Lista mahdollisista tapahtumista. Tapahtumat pitää olla prefabbeina.
	public List<Transform> eventGroup1 = new List<Transform>();
	int events1 = 0; // Montako ryhmän 1 tapahtumaa on tapahtunut.
	
	public List<Transform> eventGroup2 = new List<Transform>();
	int events2 = 0;
	
	public List<Transform> eventGroup3 = new List<Transform>();
	int events3 = 0;
	
	public int eventGroupNumber = 1; // Minkä ryhmän eventtejä otetaan.
	
	public float walkSpeed = 0.5f;                // Pelaajan kävelynopeus.
	public float relativeBackgroundSpeed = 0.3f;  // Taustan suhteellinen nopeus pelaajaan.
	public float relativeBackground2Speed = 0.1f; // Taaemman taustan suhteellinen nopeus pelaajaan.
	public float eventTimer = 10.0f;              // Montako sekuntia pitää kävellä ennen kuin tapahtuma tulee.
	
	public List<string> dialogue = new List<string>(); // Menossa oleva dialogi.
	
	private float scroll = 0f; // Paljonko ollaan liikuttu.
	private float playerDir = 1f; // Pelaajan suunta, joko -1 tai 1.
	
	private GUIStyle style;

	public Font font;
	
	
	void Start () {
		style = new GUIStyle();
		style.wordWrap = true;
		style.fontSize = 20;
		style.normal.textColor = Color.white;
		style.alignment = TextAnchor.MiddleCenter;
		style.font = font;
	}
	
	void OnGUI() {
		if (dialogue.Count > 0) {
			var w = Screen.width;
			var h = Screen.height;
			var rect = new Rect(w * 0.333f, h * 0.1f, w * 0.333f, h * 0.3f);
			GUI.Box(rect, ""); // todo: lisää kuva
			GUI.Box(rect, dialogue[0], style);
		}
	}
	
	public bool PlayerCanAct() {
		return dialogue.Count == 0;
	}
	
	void Update () {
		if (dialogue.Count > 0 && Input.anyKeyDown) {
			dialogue.RemoveAt(0);
		}

		// Syötteen otto.
		float direction = Input.GetAxis("Horizontal");
		if (!PlayerCanAct()) {
			direction = 0f;
		}
		
		// Pelaajan animaation parametrit.
		var anim = player.GetComponent<Animator>();
		anim.SetFloat("PlayerSpeed", Mathf.Abs(direction));

		// Pelaajan päivitys.
		UpdatePlayer(direction);
	}
	
	void UpdatePlayer (float direction) {
		// Spriten pyöritys skaalauksella liikkumissuunnan mukaan.
		if (direction > 0.0) playerDir = 1;
		if (direction < 0.0) playerDir = -1;
		var spriteScale = player.transform.localScale;
		spriteScale.x = Mathf.Abs(spriteScale.x) * playerDir;
		player.transform.localScale = spriteScale;
		
		ground.renderer.material.SetTextureOffset("_MainTex", new Vector2(scroll, -0.005f));
		background.renderer.material.SetTextureOffset("_MainTex", new Vector2(scroll * relativeBackgroundSpeed, -0.005f));
		background2.renderer.material.SetTextureOffset("_MainTex", new Vector2(scroll * relativeBackground2Speed, 0));
		
		if (direction != 0.0) {
			var movement = walkSpeed * Time.smoothDeltaTime * direction;
			eventTimer -= Time.smoothDeltaTime;
			
			// Liikuta maata ja taustaa.
			scroll += movement;
			
			// Liikuta objekteja.
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("EventObject")) {
				Vector3 pos = obj.transform.position;
				pos.x -= movement * ground.renderer.bounds.size.x;
				obj.transform.position = pos;
				
				// Jos objekti on liian kaukana, niin se poistetaan.
				if (Mathf.Abs(obj.transform.position.x) > ground.renderer.bounds.size.x * 0.5 + 0.3) {
					Destroy(obj);
				}
			}
		}
		
		// Tapahtumien luonti.
		
		if (direction != 0.0) {
			if (eventTimer < 0.0) {
				// Seuraava tapahtuma tulee 20 - 50 sek. kävelyn kuluttua.
				//eventTimer = 20 + Random.value * 30;
				
				eventTimer = 5; // testin vuoksi 5 sek.
				
				var ev = RandomEvent();
				if (ev != null) {
					// Luo uuden tapahtuman.
					float x = Mathf.Sign(direction) * (ground.renderer.bounds.size.x * 0.5f + 0.1f);
					Instantiate(ev, new Vector3(x, 0, 0), Quaternion.identity);
				}
				else {
					// Kaikki tapahtumat ovar ilmestyneet.
					// Tässä pitäisi olla koodia joka tappaa pelaajan ja lopettaa pelin.
				}
			}
		}
	}
	
	// Luo satunnaisen tapahtuman.
	// Palauttaa null jos tapahtumia ei ole jäljellä.
	Transform RandomEvent () {
		List<Transform> eventGroup = null;
		if (eventGroupNumber == 1) {
			eventGroup = eventGroup1;
			if (++events1 >= 3)
				eventGroupNumber++;
		}
		else if (eventGroupNumber == 2) {
			eventGroup = eventGroup2;
			if (++events2 >= 3)
				eventGroupNumber++;
		}
		else if (eventGroupNumber == 3) {
			eventGroup = eventGroup3;
			if (++events3 >= 3)
				eventGroupNumber++;
		}

		if (eventGroup != null && eventGroup.Count > 0) {
			int index = Random.Range(0, eventGroup.Count - 1);
			var ev = eventGroup[index];
			eventGroup.RemoveAt(index);
			return ev;
		}

		return null;
	}

}
