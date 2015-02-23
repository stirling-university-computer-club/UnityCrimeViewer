using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;
/**
 * Script to parse the data that is coming from the csv attached.
 * @author C.W.Docherty
 * @date 21/02/15
 * */
public class parserMain : MonoBehaviour {
	
	//csv file.
	public TextAsset theFile;
	private int rows;//number of rows in the file
	private int columns;//number of columns in the file.
	public Visualiser mushniks;//how we speak to the visualiser!
	public clusterScript cluster;
	public TextAsset forecastPolice;
	
	
	//colour variables
	public byte r,g,b;
	public string id;
	public int index;
	private string[] titles; //the headline titles for the data.
	private string[] rawData;//array to hold the extracted data.
	private string[] modifiedData;//the cut data.
	private string[] actualData;
	
	private string[] policeTitles;
	private string[] policeData;
	private string[] modifiedPoliceData;
	private int policeRows;
	private int policeColumns;
	private string[] policeRawData;
	
	private bool active; //does the data need to be extracted or not.
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void startMe (string id, float xModifier){
		this.id = id;
		//if (this.index == checkIndex) {
		activateDataExtraction (xModifier);
		//}
	}
	
	/**
	 * Extracts the data from the attached CSV file.
	 * */
	public void activateDataExtraction(float xModifier){
		//we need to determine the number of rows and columns in the csv file.
		//The first Row will hold this information.
		
		string tomatch = "\r\n";
		
		int counter = 2;
		
		
		//extract the data
		rawData = theFile.text.Split (new string[]{",",tomatch},StringSplitOptions.RemoveEmptyEntries);
		
		//Assign rows and columns their values.
		rows = int.Parse(rawData [0]);
		columns = int.Parse(rawData [1]);
		
		//initialise the array to the number of rows and columns multiplied together.
		actualData = new string[(rows * columns)];
		
		//copy the data we want from the raw data to separate the titles & row/column info from the data.
		Array.Copy(rawData,((columns)+2),actualData,0,(rawData.Length-(columns)-2));
		
		
		//initialise titles
		titles = new string[columns];
		
		for (int x = 0; x <columns; x++) {
			titles[x] = rawData[counter];
			counter++;
		}
		activatePoliceDataExtraction ();
		speakToVisualiser (xModifier);
		
		
	}
	
	public void activatePoliceDataExtraction(){
		//we need to determine the number of rows and columns in the csv file.
		//The first Row will hold this information.
		string tomatch = "\r\n";
		
		int counter = 2;
		
		
		//extract the data
		policeRawData = forecastPolice.text.Split (new string[]{",",tomatch},StringSplitOptions.RemoveEmptyEntries);
		
		//Assign rows and columns their values.
		policeRows = int.Parse(policeRawData [0]);
		policeColumns = int.Parse(policeRawData [1]);
		
		
		//initialise the array to the number of rows and columns multiplied together.
		policeData = new string[(policeRows * policeColumns)];
		
		//copy the data we want from the raw data to separate the titles & row/column info from the data.
		Array.Copy(policeRawData,((policeColumns)+2),policeData,0,(policeRawData.Length-policeColumns)-2);
		
		
		//initialise titles
		policeTitles = new string[policeColumns];
		
		for (int x = 0; x <policeColumns; x++) {
			policeTitles[x] = policeRawData[counter];
			counter++;
		}
		
		for (int x =0; x<policeData.Length; x++) {
			Debug.Log ("Element: " + x + " - status: " + policeData [x]);
		}
		
		
	}
	private void speakToVisualiser(float xModifier){
		mushniks.feedMe (titles,actualData,r,g,b,index,xModifier,policeTitles,policeData,id);
	}
}
