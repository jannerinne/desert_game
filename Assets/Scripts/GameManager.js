#pragma strict
import System.Collections.Generic;

var player : GameObject;
var ground : GameObject;
var background : GameObject;
var background2 : GameObject;

// Lista mahdollisista tapahtumista. Tapahtumat pitää olla prefabbeina.
var eventGroup1 : List.<Transform> = List.<Transform>();
var events1 : int = 0; // Montako ryhmän 1 tapahtumaa on tapahtunut.

var eventGroup2 : List.<Transform> = List.<Transform>();
var events2 : int = 0;

var eventGroup3 : List.<Transform> = List.<Transform>();
var events3 : int = 0;

var eventGroupNumber : int = 1; // Minkä ryhmän eventtejä otetaan.

var walkSpeed : float = 0.5;                // Pelaajan kävelynopeus.
var relativeBackgroundSpeed : float = 0.3;  // Taustan suhteellinen nopeus pelaajaan.
var relativeBackground2Speed : float = 0.1; // Taaemman taustan suhteellinen nopeus pelaajaan.
var eventTimer : float = 10.0;              // Montako sekuntia pitää kävellä ennen kuin tapahtuma tulee.

private var scroll : float; // Paljonko ollaan liikuttu.
private var playerDir : float = 1; // Pelaajan suunta, joko -1 tai 1.

function Start () {
}

function Update () {

	// ------- Liikkuminen -------
	var direction = Input.GetAxis("Horizontal");
	
	// Pelaajan animaation parametrit.
	var anim = player.GetComponent(typeof(Animator));
	anim.SetFloat("PlayerSpeed", Mathf.Abs(direction));
	
	// Spriten pyöritys skaalauksella liikkumissuunnan mukaan.
	if (direction > 0.0) playerDir = 1;
	if (direction < 0.0) playerDir = -1;
	var spriteScale = player.transform.localScale;
	spriteScale.x = Mathf.Abs(spriteScale.x) * playerDir;
	player.transform.localScale = spriteScale;
	
	ground.renderer.material.SetTextureOffset("_MainTex", Vector2(scroll, -0.005f));
	background.renderer.material.SetTextureOffset("_MainTex", Vector2(scroll * relativeBackgroundSpeed, -0.005f));
	background2.renderer.material.SetTextureOffset("_MainTex", Vector2(scroll * relativeBackground2Speed, 0));
	
	if (direction != 0.0) {
		var movement = walkSpeed * Time.smoothDeltaTime * direction;
		eventTimer -= Time.smoothDeltaTime;
		
		// Liikuta maata ja taustaa.
		scroll += movement;
		
		// Liikuta objekteja.
		for (var obj : GameObject in GameObject.FindGameObjectsWithTag("EventObject")) {
			obj.transform.position.x -= movement * ground.renderer.bounds.size.x;
			
			// Jos objekti on liian kaukana, niin se poistetaan.
			if (Mathf.Abs(obj.transform.position.x) > ground.renderer.bounds.size.x * 0.5 + 0.3) {
				Destroy(obj);
			}
		}
	}
	
	// ------- Tapahtumien luonti -------
	
	if (direction != 0.0) {
		if (eventTimer < 0.0) {
			// Seuraava tapahtuma tulee 20 - 50 sek. kävelyn kuluttua.
			//eventTimer = 20 + Random.value * 30;
			
			eventTimer = 5; // testin vuoksi 5 sek.
			
			var event = RandomEvent();
			if (event != null) {
				// Luo uuden tapahtuman.
				var x = Mathf.Sign(direction) * (ground.renderer.bounds.size.x * 0.5 + 0.1);
				var obj = Instantiate(event, Vector3(x, 0, 0), Quaternion.identity);
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
function RandomEvent () {
    var eventGroup : List.<Transform> = null;
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
		var index : int = Random.Range(0, eventGroup.Count - 1);
		var event = eventGroup[index];
		eventGroup.RemoveAt(index);
		return event;
	}
	return null;
}
