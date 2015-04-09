using UnityEngine;
using System.Collections;

public class Paakuva : MonoBehaviour {

	public Sprite shaun;
	public Sprite darin;
	public Sprite monique;
	public Sprite tyrone;

	private Vector3 startpos;


	// Use this for initialization
	void Start () {
		startpos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Show(Vector2 pos, string text) {
		Vector3 newpos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, Screen.height - pos.y, 0f));
		transform.position = new Vector3(newpos.x, newpos.y, 0f);

		if (text.StartsWith("Shaun")) {
			GetComponent<SpriteRenderer>().sprite = shaun;
		}
		else if (text.StartsWith("Darin")) {
			GetComponent<SpriteRenderer>().sprite = darin;
		}
		else if (text.StartsWith("Monique")) {
			GetComponent<SpriteRenderer>().sprite = monique;
		}
		else if (text.StartsWith("Tyrone")) {
			GetComponent<SpriteRenderer>().sprite = tyrone;
		}
		else {
			Hide();
		}
	}

	public void Hide() {
		transform.position = startpos;
	}
}