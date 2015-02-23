using UnityEngine;
using System.Collections;

public class clusterScript : MonoBehaviour {

	public GameObject cube;
	public GameObject sphere;

	public GameObject[]myCubes;
	public string name;
	private float radius = 1.5f ;
	private float x;
	private float z;
	private int angle;
	public Vector3 center;

	// Use this for initialization
	void Start(){

		}
	public void hi(){
		Update ();
	}
	public void init (int numCubes, Vector2 origin, int pass,string id) {
		Color theColour = Color.white;

		if (pass == 1) {
			theColour = Color.green;
		} else if (pass == 3) {
			theColour = Color.red;
		} else if (pass == 2) {
			theColour = Color.yellow;
		}

		myCubes = new GameObject[numCubes];

		sphere =  GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = new Vector3 (origin.x, 5, origin.y);
		sphere.transform.localScale = new Vector3 (1, 1, 1);
		center = sphere.transform.position;
		sphere.renderer.material.color = theColour;
		sphere.name = id;
		angle = 360 / numCubes;

		for (int i = 0; i<numCubes; i++) {
			x = (float) (radius * Mathf.Cos((angle * i) * Mathf.PI/180F)) + origin.x;
			z = (float) (radius * Mathf.Sin((angle * i) * Mathf.PI/180F)) + origin.y;
			float y = Random.Range(-2.0f,2.0f)+5.0f;
			myCubes[i] =  GameObject.CreatePrimitive(PrimitiveType.Cube);
			myCubes[i].transform.position = new Vector3(x,y,z);
			myCubes[i].transform.localScale = new Vector3 (0.1f, 3.0f, 0.1f);
			myCubes[i].renderer.material.color = theColour;
			myCubes[i].name = id;

				}


	
	}
	
	// Update is called once per frame
	void Update () {
	
		for (int i = 0; i< myCubes.Length; i++) {
			myCubes[i].transform.RotateAround (center, Vector3.up, 45 * Time.deltaTime);
				}

	}
}
