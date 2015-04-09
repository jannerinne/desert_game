using UnityEngine;
using System.Collections;

public class FpsCap : MonoBehaviour {

	void Awake() {
		Application.targetFrameRate = 30;
	}
}
