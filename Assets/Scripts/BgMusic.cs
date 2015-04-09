using UnityEngine;
using System.Collections;

public class BgMusic : MonoBehaviour {

	public AudioClip menuMusic;
	public AudioClip gameMusic;

	private AudioSource source;

	void Awake() {
		DontDestroyOnLoad(gameObject);
		
		if (FindObjectsOfType(GetType()).Length > 1) {
			Destroy(gameObject);
		}
	}
	
	void Start () {
		source = GetComponent<AudioSource>();
	}

	void Update () {	
	}

	public void PlayMenuMusic() {
		Play(menuMusic);
	}

	public void PlayGameMusic() {
		Play (gameMusic);
	}

	public void PlayWind() {
		transform.GetChild(0).GetComponent<AudioSource>().Play();
	}

	public void StopWind() {
		transform.GetChild(0).GetComponent<AudioSource>().Stop();
	}

	private void Play(AudioClip clip) {
		source.clip = clip;
		source.loop = true;
		source.Play();
	}
}
