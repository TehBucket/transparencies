using UnityEngine;
using System.Collections;

public class spawnenemy : MonoBehaviour {
	public GameObject rangedguy;
	public GameObject meleeguy;
	
		GameObject thechosen;
	
	void pickenemy()
	{
	int rando = Mathf.Abs(Random.Range(1,4));
	if(rando == 1) { thechosen = meleeguy; }
	if(rando != 1) { thechosen = rangedguy; }

	spawn();
	}
	
	void spawn()
	{
	GameObject instance = Instantiate(thechosen, transform.position, transform.rotation) as GameObject;
	destruct();
	}
	
	void destruct(){ Destroy(gameObject); }
	
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
	pickenemy ();
	}
}
