using UnityEngine;
using System.Collections;

public class teleport : MonoBehaviour {

	public GameObject teledest;
	public AudioClip telesound;
	
	void OnTriggerEnter(Collider thecollider)
	{
	if	(thecollider.gameObject != pickupwep.heldgun)
		{
			doteleportmygoodsir(thecollider.gameObject);
		}
	}
	void doteleportmygoodsir(GameObject theobject)
	{
		AudioSource.PlayClipAtPoint(telesound, transform.position);
		AudioSource.PlayClipAtPoint(telesound, teledest.transform.position);
//		GameObject player = GameObject.FindWithTag("Player");
			theobject.transform.position = teledest.transform.position;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
