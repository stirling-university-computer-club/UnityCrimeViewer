using UnityEngine;
using System.Collections;

public class dataForecast : MonoBehaviour {

	private float prediction;
	private float answer;

	private float[] policeNumbers;
	private float[] dataToBeForecast;

	private float[] policePercentageYoY;
	private float[] dataPercentageYoY;

	private float nextYearPolicePercent;
	private float nextYearDataPercentage;

	private int startModifier;
	private bool policeShoter;
	private bool dataShorter;
	// Use this for initialization
	void Start() {

	}


	public int init(string[] police, string[] data){
		policeNumbers = new float[police.Length];
		dataToBeForecast = new float[data.Length];


		for (int x = 0; x < police.Length; x++) {
			policeNumbers [x] = (float)float.Parse(police [x]);
		}

		for (int x = 0; x < data.Length; x++) {
			dataToBeForecast [x] = (float)float.Parse(data [x]);
		}

		if (dataToBeForecast.Length < policeNumbers.Length) {
			startModifier = (dataToBeForecast.Length - policeNumbers.Length);
		} else if (dataToBeForecast.Length > policeNumbers.Length) {
			startModifier = (policeNumbers.Length - dataToBeForecast.Length );
		}

		policePercentageYoY = workoutPolicePercentage ();
		dataPercentageYoY = workoutDataPercentage ();


		nextYearPolicePercentageForecast ();
		nextYearDataPercentageForecast ();
		Debug.Log ("Police: " + nextYearPolicePercent + " - Current Data: " + nextYearDataPercentage);

		if (nextYearPolicePercent > 100.0f && nextYearDataPercentage < 100.0f) {
			return 1;
		} else if(nextYearPolicePercent > 100.0f || nextYearDataPercentage < 100.0f){
			return 2;
		}else {
			return 3;
		}
		return 3;
	}

	// Update is called once per frame
	void Update () {
	
	}

	private float[] workoutPolicePercentage(){
		float[] results;
		float compare;
		if (dataShorter) {
			results = new float[policeNumbers.Length-(startModifier)];
			compare = policeNumbers[startModifier];
			for (int x = startModifier; x<policeNumbers.Length-(startModifier); x++) {
				results[x] = ((policeNumbers[x]/compare)*100);
				compare = policeNumbers[x];
			}
		} else {
			 compare = policeNumbers [0];
			results = new float[policeNumbers.Length-1];
			for (int x = 1; x<policeNumbers.Length-1; x++) {
				results[x] = ((policeNumbers[x]/compare)*100);
				compare = policeNumbers[x];
			}
		}

		return results;
	}

	private float[] workoutDataPercentage(){
		float[] results;
		float compare;
		if (policeShoter) {
			results = new float[dataToBeForecast.Length-(startModifier)];
			compare = dataToBeForecast[startModifier];
			for (int x = startModifier; x<dataToBeForecast.Length-(startModifier); x++) {
				results[x] = ((dataToBeForecast[x]/compare)*100);
				compare = dataToBeForecast[x];
			}
		} else {
			compare = dataToBeForecast [0];
			results = new float[dataToBeForecast.Length-1];
			for (int x = 1; x<dataToBeForecast.Length-1; x++) {
				results[x] = ((dataToBeForecast[x]/compare)*100);
				compare = dataToBeForecast[x];
			}
		}
		
		return results;
	}
	private void nextYearPolicePercentageForecast(){
		float total = 0.0f;

		for (int x =0; x <policePercentageYoY.Length; x++) {
			total += policePercentageYoY [x];
		}

		nextYearPolicePercent = total / policePercentageYoY.Length;
	}

	private void nextYearDataPercentageForecast(){
		float total = 0.0f;
		
		for (int x =0; x <dataPercentageYoY.Length; x++) {
			total += dataPercentageYoY [x];
		}
		
		nextYearDataPercentage = total / dataPercentageYoY.Length;
	}
}
