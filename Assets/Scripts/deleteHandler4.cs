using UnityEngine;
using System.Collections;

public class deleteHandler4 : MonoBehaviour {
	
	private bool selectionActive;
	private bool triggered;
	private string selectionName;
	public parserMain parseMe;
	public string name;
	public string index;
	private float xModifier;
	
	public deleteHandler4(string selectionName) {
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
		selectionName = "test";
		xModifier = 1.0f;
	}
	
	void Update () { 
		if (triggered) {
			float temp = Input.GetAxisRaw ("Fire1"); 
			if (!selectionActive && temp > 0) {
				setActive (true);
				foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
				{
					if(gameObj.name == "handler4")
					{
						Destroy(gameObj);
					}
				}


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
		
		information = "To remove " + name + "\npress 'e'.";
		return information;
		
	}
	
}
