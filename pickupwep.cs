using UnityEngine;
using System.Collections;

public class pickupwep : MonoBehaviour {
	static public GameObject thegun;
	static public GameObject heldgun;
	GameObject throwgun;

	public int throwforce;
	
	public static bool holdingwep = false;
	
	void OnTriggerEnter(Collider theobject) {
	//if collission with gun
		if(theobject.gameObject.tag == "wep")
		{
			if(thegun != theobject)
			{
						//puts icon on thegun
			GameObject icon = GameObject.FindWithTag("selectedicon");
			icon.transform.position = theobject.gameObject.transform.position;
			icon.transform.parent = theobject.gameObject.transform;
			}
		thegun = theobject.gameObject;
		}
	}
	
	void destructicon(){
		GameObject icon = GameObject.FindWithTag("selectedicon");
		GameObject nothereplace = GameObject.FindWithTag("nothere");
		icon.transform.parent = null;
		icon.rigidbody.MovePosition(nothereplace.transform.position);
	}
	
	void pickup()
	{
		destructicon();
		GameObject wepspacething = GameObject.FindWithTag("wepspace");
		thegun.transform.position = wepspacething.transform.position;
		thegun.transform.rotation = wepspacething.transform.rotation;
		thegun.rigidbody.isKinematic = true;
		thegun.transform.parent=wepspacething.transform;
		heldgun = thegun;
		thegun = null;
	}
	
	void throwit()
	{
	throwgun = heldgun;
	throwgun.rigidbody.isKinematic = false;
	throwgun.transform.parent = null;
	throwgun.rigidbody.AddRelativeForce(-throwforce, 0, 0);
		
	heldgun = null;
	StartCoroutine(nullifythrowngun());
	}
	
	IEnumerator nullifythrowngun()
	{
		yield return new WaitForSeconds(.5F);
		throwgun = null;
	}
	
	// Use this for initialization
	void Start () {
	heldgun = null;
	thegun = null;
	holdingwep = false;
	destructicon();
	}
	
	// Update is called once per frame
	// I see you have found me, Mr. Bond.
	void Update () {
		
				//SUPPOSED TO: throw weapon if none selected (finally works)
				//				pickup weapon if none held (works)
				//				if held AND selected, throw held then pickup selected (works
				if (Input.GetButtonDown("Fire2"))
				{
						if(holdingwep == true)
						{
							throwit ();
								if(thegun != null)
									{
							pickup ();
							holdingwep = true;	
									}
								else{holdingwep = false;}
						}
//				if(holdingwep == false)
				else
					{
					if(thegun != null)
						{
						print (thegun.name);
						if(thegun != throwgun)
							{
							pickup ();
							holdingwep = true;
							}
						}
					}
				}
	}
}
