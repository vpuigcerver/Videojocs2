using UnityEngine;
using System.Collections;

public class Tabs : MonoBehaviour {

	GameObject armasPanel; 
	GameObject armaduraPanel;
	GameObject armasTab;
	GameObject armaduraTab;


	// Use this for initialization
	void Start () {

		armasPanel = GameObject.FindGameObjectWithTag("ArmasPanel");
		armaduraPanel = GameObject.FindGameObjectWithTag("ArmaduraPanel");
		armasTab = GameObject.FindGameObjectWithTag("ArmasB");
		//armaduraTab = GameObject.FindGameObjectWithTag("ArmaduraB");

		armasPanel.gameObject.transform.position = new Vector3 (224, 65, -1);
		armaduraPanel.gameObject.transform.position = new Vector3 (224, 65, 1);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	void OnMouseDown(){
		if (this.gameObject.tag == ("ArmasB")) {
			armasPanel.gameObject.transform.position = new Vector3 (224, 65, -1);
			armaduraPanel.gameObject.transform.position = new Vector3 (224, 65, 1);
		}
		if (this.gameObject.tag == ("ArmaduraB")) {
			armasPanel.gameObject.transform.position = new Vector3 (224, 65, 1);
			armaduraPanel.gameObject.transform.position = new Vector3 (224, 65, -1);
		}
	}
}
