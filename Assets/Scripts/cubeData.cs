using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class cubeData : MonoBehaviour {
	private string title;
	private double number;

	// Use this for initialization
	void Start () {
		title = "test";
		number = 25452152;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setData(string theTitle, double theNumber){
		this.title = theTitle;
		this.number = theNumber;
	}

	void OnTriggerEnter(Collider col){
		Debug.Log (title + " - " + number);
	}

	void onTriggerExit(Collider col){
		guiText.text = "";
	}
}
