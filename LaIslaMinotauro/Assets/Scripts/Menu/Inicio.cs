using UnityEngine;
using System.Collections;

public class Inicio : MonoBehaviour {
	 
	GameObject play;
	GameObject options;
	GameObject controls;
	GameObject exit;



	// Use this for initialization
	void Start () {
		play = GameObject.FindGameObjectWithTag ("Play");
		options = GameObject.FindGameObjectWithTag ("Options");
		controls = GameObject.FindGameObjectWithTag ("Controls");
		exit = GameObject.FindGameObjectWithTag ("Exit");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){

		if (this.gameObject.tag==("Play")) {
			Debug.Log ("PLAY");
			Application.LoadLevel (1);
		}
		if (this.gameObject.tag == ("Options")) {
			Application.LoadLevel (2);
		}
		if (this.gameObject.tag == ("Controls")) {
			Application.LoadLevel (3);
		}
		if (this.gameObject.tag == ("Exit")) {
			Application.Quit();
		}
	}
}
