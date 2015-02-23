using UnityEngine;
using System.Collections;

public class Visualiser : MonoBehaviour
{
	
	public  GameObject cube;
	//	public  GameObject sphere;
	public Transform center;
	private int variables = 5;	//number of input variables
	private int xGap = 0;	//gap between columns
	private float xSize = 1.0f;
	private float ySize = 1.0f;
	private float zSize = 1.0f;
	private Color32 cubeColour;
	private float [] zPos;
	private bool [] active;
	public clusterScript[] cluster;
	public Font font;
	public dataForecast dFore;
	private string id;
	
	// Use this for initialization
	void Start ()
	{
		//for (int x = 0; x <= variables; x++) {					
		active = new bool[]{false,false,false,false,false};
		zPos = new float[5];
		
		zPos [0] = 1.0f;
		zPos [1] = 5.0f;
		zPos [2] = 9.0f;
		zPos [3] = 13.0f;
		zPos[4] = 17.0f;
		
		cluster = new clusterScript[5];
		
		
		
		//	xGap++;
		//}			
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int x = 0; x < 5; x++) {
			if(cluster[x] != null){
				cluster[x].hi();
			}
		}
		
	}
	public void createCube(float xPos,float zPos,float sizeMod,byte r,byte g,byte b, string title, double data, float xModifier, string id){
		float xpos,zpos,ymod;
		xpos = xPos;
		zpos = zPos;
		ymod = (10 * sizeMod);
		cubeColour = new Color32 (r, g, b, 1);
		cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.transform.position = new Vector3 (xpos, 0, (zpos));
		cube.renderer.material.color = cubeColour;
		cube.transform.localScale = new Vector3 (xSize, ymod, zSize);
		cube.name = id;
		
		addHeading (title,xpos,zpos,ymod,xModifier, id);
		addNumericalData (data,xpos,zpos,ymod,xModifier, id);
		
	}
	
	private void addHeading(string title,float x, float z, float y, float xModifier,string id){
		GameObject textObject = new GameObject ("text");
		textObject.AddComponent ("TextMesh");
		textObject.name = id;
		Font test = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
		TextMesh textMeshComponent = textObject.GetComponent (typeof(TextMesh)) as TextMesh;
		textMeshComponent.text = title;
		textMeshComponent.transform.rotation = Quaternion.AngleAxis (90, Vector3.forward);
		textMeshComponent.transform.position = new Vector3 ((x-0.5f), (y+1.0f), z);
		textMeshComponent.font = test;
		textMeshComponent.fontSize = 10;
		if (xModifier == 1.0f) {
			textMeshComponent.color = Color.black;
		} else if (xModifier == 1.25f) {
			textMeshComponent.color = Color.white;
		}else if (xModifier == 1.50f) {
			textMeshComponent.color = Color.red;
		}else if (xModifier == 1.75f) {
			textMeshComponent.color = Color.yellow;
		}else if (xModifier == 2.0f) {
			textMeshComponent.color = Color.magenta;
		}
		textMeshComponent.renderer.material = test.material;
		textMeshComponent.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		textMeshComponent.fontSize = 100;
	}
	
	private void addNumericalData(double data ,float x, float z, float y,float xModifier,string id){
		GameObject textObject = new GameObject ("text");
		textObject.AddComponent ("TextMesh");
		textObject.name = id;
		Font test = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
		TextMesh textMeshComponent = textObject.GetComponent (typeof(TextMesh)) as TextMesh;
		textMeshComponent.text = data.ToString("0");
		textMeshComponent.transform.rotation = Quaternion.AngleAxis (90, Vector3.forward);
		textMeshComponent.transform.position = new Vector3 ((x-0.5f), (y -(y*0.85f)), (z-0.5f));
		textMeshComponent.font = test;
		textMeshComponent.fontSize = 10;
		if (xModifier == 1.0f) {
			textMeshComponent.color = Color.black;
		} else if (xModifier == 1.25f) {
			textMeshComponent.color = Color.white;
		}else if (xModifier == 1.50f) {
			textMeshComponent.color = Color.red;
		}else if (xModifier == 1.75f) {
			textMeshComponent.color = Color.yellow;
		}else if (xModifier == 2.0f) {
			textMeshComponent.color = Color.magenta;
		}
		textMeshComponent.renderer.material = test.material;
		textMeshComponent.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		textMeshComponent.fontSize = 100;
	}
	public void feedMe(string[] titles, string[] data, byte r, byte g, byte b,int index, float xModifier, string[] policeTitles, string[] policeData,string id){
		float xPos; //the x and z offsets for the blocks!
		xPos = 1.0f;
		this.id = id;
		
		active [index] = true;
		
		//we need to create the blocks here.
		//get the right order of largest to smallest.
		double[] largestToSmallest = turnToInt (data);
		int largeToSmall = workOutLargest (largestToSmallest);
		//determine the size modifiers.
		float[] sizeModifiers = getSizeModifiers (largestToSmallest, largeToSmall);
		
		
		
		if (active [index]) {
			int pass = dFore.init(policeData,data);
			
			float actualZ;
			//now we will create the cubes!
			cluster[index] = new clusterScript();
			cluster[index].init (titles.Length, new Vector2 (45.0f,zPos[index]),pass,id);
			for (int x = 0; x<data.Length; x++) {
				
				createCube (xPos, zPos[index], sizeModifiers [x], r, g, b, titles[x], largestToSmallest[x], xModifier, id);
				xPos = xPos + 3.0f;
			}
			
		}
		
		Vector3 initial = new Vector3 (0, 0, 0);
		
	}
	
	private int workOutLargest(double[] data){
		double[] results = new double[data.Length];
		double [] tempStore = new double[data.Length];
		int index;
		
		
		index = 0;
		for (int x = 1; x< tempStore.Length-1; x++) {
			if (tempStore [x + 1] > tempStore [x]) {
				index = x;
			}
		}
		return index;
	}
	
	private double[] turnToInt(string[] data){
		double[] results = new double[data.Length];
		
		for(int x = 0; x < results.Length;x++){
			results[x] = double.Parse(data[x]);
		}
		
		return results;
	}
	
	private float[] getSizeModifiers(double[] data, int index){
		float[] results = new float[data.Length];
		
		//first element is always going to be the largest!
		results [index] = 1.0f;
		
		//create the rest of the sizes!
		for (int x = 0; x<data.Length; x++) {
			if (x != index) {
				results [x] = (float)(data [x] / data [0]);
			}
		}
		return results;
		
	}
}
