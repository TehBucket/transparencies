using UnityEngine;
using System.Collections;

public class simpledie : MonoBehaviour {
	
	public float thedelay = .8F; 
	
	
		IEnumerator diedelay() {
	yield return new WaitForSeconds(thedelay);
		Destroy(gameObject);
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	StartCoroutine(diedelay());
	}
}
