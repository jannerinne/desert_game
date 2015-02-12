#pragma strict

function Start () {

}

function Update () {
	// Jos painetaan välilyöntiä ja kappale on tarpeeksi lähellä ruudun keskikohtaa
	// niin poistetaan objekti.
	if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(transform.position.x) < 1.0) {
		Destroy(this.gameObject);
	}
}