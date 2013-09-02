using UnityEngine;
using System.Collections;

public class weaponspawn : MonoBehaviour {
	
	
	public GameObject wallhacklaser;
	public GameObject grenadelauncher;
	public GameObject shotgun;
	public GameObject fastrifle;
	public GameObject slowrifle;
	public GameObject knife;
	public GameObject baton;
		
	GameObject theweapon;

		
	void pickweapon(){
		//melee or ranged wep
	int wep1 = Mathf.Abs(Random.Range(1,3));
		//random ranged
		if (wep1 != 1)
		{
		int wep2 = Mathf.Abs(Random.Range(1,14));
			if(wep2 == 1)
			{
				theweapon = wallhacklaser;
			}
			if(wep2 == 2)
			{
				theweapon = grenadelauncher;
			}
			if(wep2 == 3 || wep2 == 4 || wep2 == 5 || wep2 == 6)
			{
				theweapon = shotgun;
			}
			if(wep2 == 7 || wep2 == 8 || wep2 == 9 || wep2 == 10)
			{
				theweapon = fastrifle;
			}
			if(wep2 == 11 || wep2 == 12 || wep2 == 13 || wep2 == 14)
			{
				theweapon = slowrifle;
			}
		}
		//random ranged
		if (wep1 == 1)
		{
		int wep2 = Mathf.Abs(Random.Range(1,3));
			if(wep2 != 1)
			{
				theweapon = knife;
			}
			if(wep2 == 1)
			{
				theweapon = baton;
			}
			
			
		}
		spawnweapon();
		
	}
	
	//create the picked weapon
	void spawnweapon()
		{
		GameObject instance = Instantiate(theweapon, transform.position, transform.rotation) as GameObject;
			
		destruct();
		}
	
	void destruct(){ Destroy(gameObject); }
	
	// Use this for initialization
	void Start () {
					



			
	}
	
	// Update is called once per frame
	void Update () {
		pickweapon ();
	}
}
