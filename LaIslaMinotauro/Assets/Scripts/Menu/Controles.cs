using UnityEngine;
using System.Collections;

public class Controles : MonoBehaviour {

	GameObject exit;

	// Use this for initialization
	void Start () {
		exit = GameObject.FindGameObjectWithTag ("Exit");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		if (this.gameObject.tag == ("Exit")) {
			Application.LoadLevel (0);
		}
	}
}
