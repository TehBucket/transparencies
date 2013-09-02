using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	
	bool open;
	public AudioClip doorsound;
	
	void OnTriggerEnter(Collider thecollider)
	{
	if	(thecollider.gameObject.tag == "Player")
		{
			if(open == false)
		{
			open = true;
			moveout();
		}
		}
	}
	
		void OnTriggerExit(Collider thecollider){
				if(thecollider.gameObject.tag == "Player")
		{
		if(open = true)
		{
	open = false;	
	moveback();
	}}}
	
	void moveout(){
		transform.Translate(0,5,0);
		AudioSource.PlayClipAtPoint(doorsound, transform.position);
	}
	
		
	void moveback(){
		transform.Translate(0,-5,0);
		AudioSource.PlayClipAtPoint(doorsound, transform.position);
	}

	
	
	// Use this for initialization
	void Start () {
	open = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(open == true)
		{
		}
		
		
		
	}
}
