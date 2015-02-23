using UnityEngine;
using System.Collections;

public class SelectionHandler4 : MonoBehaviour {
	
	private bool selectionActive;
	private bool triggered;
	private string selectionName;
	public parserMain parseMe;
	public string name;
	public string id;
	private float xModifier;
	
	public SelectionHandler4(string selectionName) {
		this.selectionName = selectionName;
	}
	
	public void setActive(bool selectionActive) {
		this.selectionActive = selectionActive;
	}
	
	public string toString () {
		return "Name is " + selectionName + " Active is currently " + 
			selectionActive + " Trigger is currently" + triggered;
	}
	
	// Use this for initialization
	void Start () {
		id = "handler4";
		selectionName = "test";
		xModifier = 1.75f;
	}
	
	void Update () { 
		if (triggered) {
			float temp = Input.GetAxisRaw ("Fire1"); 
			if (!selectionActive && temp > 0) {
				setActive (true);
				parseMe.startMe (id,xModifier);
			} else if (selectionActive && temp < 0) {
				setActive (false);
			}
		}
	}
	
	//When the trigger is entered.
	void OnTriggerEnter(Collider col){
		triggered = true;
	}
	
	
	//When you exit the trigger.
	void OnTriggerExit(Collider col){
		triggered = false;
	}
	
	public void OnGUI(){
		if (triggered) {
			GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 20, 300, 40), fillLabel ());
		}
	}
	
	string fillLabel(){
		
		string information;
		
		information = "To view " + name + "\npress 'e'.";
		return information;
		
	}
	
}
