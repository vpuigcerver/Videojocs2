using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {


	GameObject cruzMusic;
	GameObject cruzSound;
	GameObject onMusic;
	GameObject onSound;
	GameObject offMusic;
	GameObject offSound;
	GameObject exit;
	// Use this for initialization
	void Start () {
		
		cruzMusic = GameObject.FindGameObjectWithTag ("cruzMusic");
		cruzSound = GameObject.FindGameObjectWithTag ("cruzSound");
		onMusic = GameObject.FindGameObjectWithTag ("onMusic");
		onSound = GameObject.FindGameObjectWithTag ("onSound");
		offMusic = GameObject.FindGameObjectWithTag ("offMusic");
		offSound = GameObject.FindGameObjectWithTag ("offSound");
		exit = GameObject.FindGameObjectWithTag ("Exit");

		cruzMusic.gameObject.transform.position = new Vector3 (162, 182, 30);
		cruzSound.gameObject.transform.position = new Vector3 (162, -72, 30);
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	void OnMouseDown(){
		if (this.gameObject.tag == ("onMusic")) {
			cruzMusic.gameObject.transform.position = new Vector3 (162, 182, 30);
		}
		if (this.gameObject.tag == ("offMusic")) {
			
			cruzMusic.gameObject.transform.position = new Vector3 (562, 182, 30);
		}
		if (this.gameObject.tag == ("onSound")) {
			cruzSound.gameObject.transform.position = new Vector3 (162, -72, 30);
		}
		if (this.gameObject.tag == ("offSound")) {
			
			cruzSound.gameObject.transform.position = new Vector3 (562, -72, 30);
		}
		if (this.gameObject.tag == ("Exit")) {
			Debug.Log ("Exit");
			Application.LoadLevel (0);
		}

	}

}
