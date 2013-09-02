using UnityEngine;
using System.Collections;

public class dontdestroyonload : MonoBehaviour {
     
    void Start()
    {

		    DontDestroyOnLoad(gameObject);

	}
	void Update()
	{
	GameObject camera = GameObject.FindWithTag("MainCamera");
	transform.position	= camera.transform.position;
	}
}