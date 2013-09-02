using UnityEngine;
using System.Collections;

public class wonthegame : MonoBehaviour {
	//you lost the game
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.R))
		{
		Application.LoadLevel(1);	
		}
	}
}
