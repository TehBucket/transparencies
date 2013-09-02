using UnityEngine;
using System.Collections;

public class gui : MonoBehaviour {
	
	public Texture crosshair;
	
	
	void OnGUI()
	{
	GUI.DrawTexture(new Rect((Screen.width - crosshair.width) / 2, (Screen.height - crosshair.height) /2, crosshair.width, crosshair.height), crosshair);	
		
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
//	position = 

	}
}
