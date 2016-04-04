using UnityEngine;
using System.Collections;

public class Armas : MonoBehaviour {

	GameObject arma1;
	GameObject arma2; 
	GameObject arma3;


	// Use this for initialization
	void Start () {
		arma1 = GameObject.FindGameObjectWithTag ("arma1");
		arma2 = GameObject.FindGameObjectWithTag ("arma2");
		arma3 = GameObject.FindGameObjectWithTag ("arma3");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown(){
		if (this.gameObject.tag == ("arma1")) {

			if (General.recurso1 <= 0 || General.recurso2 <= 0 || General.recurso3 <= 0) {
				Debug.Log ("No tienes suficientes recursos");
			}
			else{
				General.recurso1 =General.recurso1 - 8;
				General.recurso2 = General.recurso2 - 2;
				General.recurso3 = General.recurso3 -3;
				Debug.Log ("Recurso1: " + General.recurso1 + " Recurso2: " + General.recurso2 + " Recurso3: " + General.recurso3);
			}

		}
		else if (this.gameObject.tag == ("arma2")) {

			if (General.recurso1 <= 0 || General.recurso2 <= 0 || General.recurso3 <= 0) {
				Debug.Log ("No tienes suficientes recursos");
			}
			else{
				General.recurso1 =General.recurso1 - 4;
				General.recurso2 = General.recurso2 - 3;
				General.recurso3 = General.recurso3 -1;
				Debug.Log ("Recurso1: " + General.recurso1 + " Recurso2: " + General.recurso2 + " Recurso3: " + General.recurso3);
			}

		}
		else if (this.gameObject.tag == ("arma3")) {

			if (General.recurso1 <= 0 || General.recurso2 <= 0 || General.recurso3 <= 0) {
				Debug.Log ("No tienes suficientes recursos");
			}
			else{
				General.recurso1 =General.recurso1 - 5;
				General.recurso2 = General.recurso2 - 4;
				General.recurso3 = General.recurso3 -6;
				Debug.Log ("Recurso1: " + General.recurso1 + " Recurso2: " + General.recurso2 + " Recurso3: " + General.recurso3);
			}

		}

	}

}
