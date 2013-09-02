using UnityEngine;
using System.Collections;

public class weapon : MonoBehaviour {
	
	//REMINDER:
	// kitty litter
	// go outside
	
	public int raynum;
	public float raydist;
	public int ammo;
	public float delayafter;
	public float spread;
	public float raylength = .01F;
	public float raywidth1 = .04F;
	public float raywidth2 = .04F;
	public float rayfadedelay = .05F;
	
	public bool israygun = false;
	public bool isglauncher = false;
	public bool islaser = false;
	public bool ismelee = false;
	public bool isautomatic = false;
	
	public GameObject projectile;
	public GameObject thechild;
	public GameObject muzzle;
	
	public AudioClip shotaudioclip;
			
	public bool activegun = false;
	
	Vector3 playerpoint;
	
    RaycastHit hit;
	RaycastHit hiti;
	
	LineRenderer line;
	
	public static GameObject hitenemy = null;
	
	public LayerMask mask;
	

	
	int whichshot = 1;
	bool shootpause = false;
	
	//WHICH SHOOT
	void shoot()
	{
		if(isautomatic == true)
			{
			if(Input.GetButton("Fire1"))
				{
					if(shootpause == false)
					{
						thechild.animation.Play ("shot"+whichshot);
						playerpoint = GameObject.FindWithTag("MainCamera").transform.position;
						AudioSource.PlayClipAtPoint(shotaudioclip, playerpoint);
						whichshot++;

						if(israygun) { shootray(); }
						StartCoroutine(waitforpause());
					}
				}
				
			}
		else if(ismelee == true)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				if(shootpause == false)
				{
				thechild.animation.Stop();
				thechild.animation.Play ("attack");
				playerpoint = GameObject.FindWithTag("MainCamera").transform.position;
				AudioSource.PlayClipAtPoint(shotaudioclip, playerpoint);
				shootmelee ();
				StartCoroutine(waitforpause());
				}
			}
		}
	else
		{
		if(Input.GetButtonDown("Fire1"))
			{
				thechild.animation.Play ("shot"+whichshot);	
				playerpoint = GameObject.FindWithTag("MainCamera").transform.position;
				AudioSource.PlayClipAtPoint(shotaudioclip, playerpoint);				
				if(isglauncher){ grenadelaunch(); }
				if(israygun){ shootray(); }
				if(islaser){ StartCoroutine(shootlaserdelay()); }
				whichshot++;
			}
		}
	}
	
	//GRENADE-LAUNCHER
	void grenadelaunch()
	{
		GameObject grenade = Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation) as GameObject;
		grenade.rigidbody.AddRelativeForce(-2000, 0, 0);
	}
	
	//FIRE-RAYS
	void shootray()
	{
		GameObject maincamera = GameObject.FindWithTag("MainCamera");
		float whichi = .01F;

			//for loop amount of rays
			for(int i = 0; i < raynum; i++)
			{
	
			float randspreadx = Mathf.Abs(Random.Range(-spread,spread));
			float randspready = Mathf.Abs(Random.Range(-spread,spread));
	
			Vector3 straight = maincamera.transform.TransformDirection(randspreadx, randspready, 1);
	        if (Physics.Raycast(maincamera.transform.position, straight, out hit, 1000, mask))
			{
				GameObject line = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
				bulletrender.point1 = muzzle.transform.position;
				bulletrender.point2 = hit.point;
				bulletrender.laserwidth1 = raywidth1;
				bulletrender.laserwidth2 = raywidth2;
				bulletrender.fadedelay = rayfadedelay;

				
	            Debug.DrawLine(muzzle.transform.position, hit.point, Color.red, 2);
	            Debug.DrawLine(maincamera.transform.position, hit.point, Color.blue, 2);
				
				if(hit.transform.tag == "enemy")
					{
					GameObject theenemy = hit.transform.gameObject;
					StartCoroutine(killallhit(theenemy, whichi));
					whichi = whichi + .1F;
					}
			}
		}
	}
	
		//MELEE - RAY
	void shootmelee()
	{
		GameObject maincamera = GameObject.FindWithTag("MainCamera");
		
		for(int i = 0; i < raynum; i++)
		{
	    RaycastHit hit;
		
		float randspreadx = Mathf.Abs(Random.Range(-spread,spread));
		float randspready = Mathf.Abs(Random.Range(-spread,spread));

		Vector3 straight = maincamera.transform.TransformDirection(randspreadx, randspready, 1);
        if (Physics.Raycast(maincamera.transform.position, straight, out hit, 1000, mask))
		{
            Debug.DrawLine(maincamera.transform.position, hit.point, Color.blue, 2);

			if(hit.transform.tag == "enemy")
				{
				float enemydist = Vector3.Distance(maincamera.transform.position, hit.transform.position);
					if(enemydist <= raydist)
					{
					GameObject instance = Instantiate(projectile, hit.transform.position, hit.transform.rotation) as GameObject;

					hitenemy = hit.transform.gameObject;
					}
				}
		}
		}
	}
	
	//WALLHACK RAY
	IEnumerator shootlaserdelay() {
	yield return new WaitForSeconds(delayafter);
	shootlaser();
	}
	
	void shootlaser()
	{
		GameObject maincamera = GameObject.FindWithTag("MainCamera");
	    RaycastHit[] hits;
		
		float whichi = .1F;
		Vector3 straight = maincamera.transform.TransformDirection(0, 0, 1);
		//if it hits i sits! :P
        hits = (Physics.RaycastAll(maincamera.transform.position, straight));
			for(int i = 0; i < hits.Length; i++)
			{
	            hiti = hits[i];
				if(hiti.transform.tag == "enemy")
				{
				GameObject theenemy = hiti.transform.gameObject;
				StartCoroutine(killallhit(theenemy, whichi));
				whichi = whichi + .1F;
				}
			
			//render line
			if(i == hits.Length - 1){
								GameObject line = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
					bulletrender.point1 = muzzle.transform.position;
					bulletrender.point2 = hiti.point;
					bulletrender.laserwidth1 = raywidth1;
					bulletrender.laserwidth2 = raywidth2;
					bulletrender.fadedelay = rayfadedelay;
					Debug.DrawLine(maincamera.transform.position, hiti.point, Color.blue, 2);
						}
			}
	}
	
	IEnumerator killallhit(GameObject enemy, float delay){
	yield return new WaitForSeconds(delay);
	hitenemy = enemy;
	Debug.Log(enemy.name);
	}
	

		
	// Use this for initialization
	void Start () {
		activegun = false;	

	}
	
	IEnumerator waitforpause() {
	shootpause = true;
	yield return new WaitForSeconds(delayafter);
	shootpause = false;
	}
	
	
	// Update is called once per frame
	void Update () {
//		if(activegun == true)
		if(this.gameObject == pickupwep.heldgun)
		{
				if(whichshot <= ammo){ shoot ();	}
		}
		else{ if(ismelee == true){ thechild.animation.Play("throwposition");}}
	
	}
}
