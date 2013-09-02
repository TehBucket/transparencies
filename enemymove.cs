using UnityEngine;
using System.Collections;

public class enemymove : MonoBehaviour {
	
	public float seeplayerdist;
	public float runspeed;
	public float walkspeed;
	public float loseplayerdist;
	public float shootdist;
	public float theshootdelay;
	public float aimdelay;
	public float stundelay;
	
	
	public GameObject child;
	public GameObject leftmuzzle;
	public GameObject rightmuzzle;
	public GameObject particles;
	public GameObject explodeparticles;
	public GameObject stunparticles;
	
	public AudioClip shootsound;
	public AudioClip diesound;
	public AudioClip explodesound;
	
	public LayerMask themask;
	
	public bool meleeguy;
	
	string aimode = "nuetral";
	bool lefthandshoot = true;
	bool shootdelay = false;
	bool hasaimed = false;
	GameObject player;
	bool newangletime = true;
	bool tryingtopathfind = false;
		
	
    void OnCollisionEnter(Collision theobject) 
	{
//		if(aimode == "nuetral")
//		{
//		newangletime = true;
//		}
		if(aimode != "dead")
		{
		if(theobject.transform.tag == "wep")
		{
		aimode = "stunned";
		}
		}
	}
	
	
	//NUETRAL
	void nuetralmove()
	{
	player = GameObject.FindWithTag("Player");
	child.animation.Play("Walk");
	float playerdist = Vector3.Distance(transform.position, player.transform.position);
	RaycastHit testhit;
			Physics.Linecast (transform.position, player.transform.position, out testhit, themask);
				
				randomwalk();

				Debug.DrawLine(transform.position, player.transform.position, Color.green, .01F);
				Debug.DrawLine(transform.position, testhit.point, Color.red, .01F);
				if(testhit.transform.tag == "Player")
					{
					aimode = "chase";
					child.animation.Stop ();
					}
	}
	
	//walk randomly
	void randomwalk()
	{
		if(newangletime == true)
		{
		float randomangle = Mathf.Abs(Random.Range(0, 90));
		StartCoroutine(waitnewangle());
		transform.rotation = Quaternion.AngleAxis(randomangle, Vector3.up);
		}
		transform.Translate(0, 0, Time.deltaTime*walkspeed);
		
	}
	
	IEnumerator waitnewangle() {
		newangletime = false;
		float randomseconds = Mathf.Abs(Random.Range(1,6));
	yield return new WaitForSeconds(randomseconds);
		newangletime = true;
	}
	
	//maybe change dir when run into wall
	void changedirection()
	{

		float randomangle = Mathf.Abs(Random.Range(0, 90));
		transform.rotation = Quaternion.AngleAxis(randomangle, Vector3.up);
		StartCoroutine(waitchangedirection());
	}
	
	IEnumerator waitchangedirection() {
	tryingtopathfind = true;
	yield return new WaitForSeconds(2);
	tryingtopathfind = false;
	}
	
	//CHASE
	void chasemove()
	{	
	player = GameObject.FindWithTag("Player");
	
	if(tryingtopathfind == false)
		{
		transform.LookAt(player.transform.position);
		}
	transform.Translate(0, 0, Time.deltaTime*runspeed);
	child.animation.Play ("Run");
	
	float playerdist = Vector3.Distance(transform.position, player.transform.position);
		
		if(playerdist > loseplayerdist)
		{
		aimode = "nuetral";
		}
		
		if(playerdist < shootdist)
			{
				if(shootdelay == false)
				{
					aimode = "isshooting";
					child.animation.Stop();
					StartCoroutine(startaiming());
				}		
			}

	}
	
	//SHOOTING WAITING
	void shootprepare()
	{
		player = GameObject.FindWithTag("Player");
		
		transform.LookAt(player.transform.position);
			if(lefthandshoot == true)
			{
			child.animation.Play ("Shoot2_mirror");
			}
			if(lefthandshoot == false)
			{
			child.animation.Play ("Shoot2");
			}
		if(hasaimed == true)
		{
			if(aimode != "dead")
			{
			shoot ();
			}
			}
	}
	
	//SHOOT
	void shoot()
	{
				player = GameObject.FindWithTag("Player");
		//Ray stuff
								if(aimode != "dead")
						{
			RaycastHit hit;
					Physics.Linecast (transform.position, player.transform.position, out hit, themask);
						if(meleeguy == true)
			{
				
			}
			else{
							if(lefthandshoot == true)
								{
								Debug.DrawLine(leftmuzzle.transform.position, hit.point, Color.blue, 1);
								GameObject particleattack = Instantiate(particles, leftmuzzle.transform.position, transform.rotation) as GameObject;
								}
							if(lefthandshoot == false)
								{
								Debug.DrawLine(leftmuzzle.transform.position, hit.point, Color.blue, 1);
								GameObject particleattack = Instantiate(particles, rightmuzzle.transform.position, transform.rotation) as GameObject;
								}
			}
							
							AudioSource.PlayClipAtPoint(shootsound, transform.position);
		
							Debug.DrawLine(transform.position, player.transform.position, Color.blue, 1);

		if(hit.transform.tag == "Player")
							{
			
							win.playerhealth --;
							}
		aimode = "shootplaying";
		lefthandshoot = !lefthandshoot;
		StartCoroutine(finishshootanim());
						}
	}
	
	
	
	IEnumerator doshootdelay() {
	shootdelay = true;
	yield return new WaitForSeconds(theshootdelay);
	shootdelay = false;
	}
	
	IEnumerator finishshootanim() {
	shootdelay = true;
	yield return new WaitForSeconds(2.41F);
	
		if(aimode != "dead"){aimode = "chase";}
	StartCoroutine(doshootdelay());
	}
	
	IEnumerator startaiming() {
	hasaimed = false;
	yield return new WaitForSeconds(aimdelay);
	hasaimed = true;
	}
	
	//DIE
	void thisdie()
	{
				aimode = "dead";
		rigidbody.isKinematic = true;
		Destroy(this.collider);
		child.animation.Stop();
		int random = Mathf.Abs(Random.Range(1, 2));
			Debug.Log(random);
			if(random == 1)
			{
			child.animation.Play("Death1");
			}
			if(random == 2)
			{
			child.animation.Play("Death2");
			}
		StartCoroutine(diedelay());
		AudioSource.PlayClipAtPoint(diesound, transform.position);	
	}
	
	IEnumerator diedelay() {
	yield return new WaitForSeconds(1.57F);
	child.animation.Stop();
	yield return new WaitForSeconds(.4F);
	GameObject particles = Instantiate(explodeparticles, transform.position, transform.rotation) as GameObject;
	Destroy(gameObject);
	AudioSource.PlayClipAtPoint(explodesound, transform.position);

	}
	
	IEnumerator stunwait() {
	GameObject particles = Instantiate(stunparticles, transform.position, transform.rotation) as GameObject;
	yield return new WaitForSeconds(stundelay);
	Destroy(particles);
	if(aimode != "dead")
		{
	aimode = "nuetral";
		}
		}
	
	//keeps rotation
	void playingshoot()
	{
	player = GameObject.FindWithTag("Player");
	transform.LookAt(player.transform.position);
	}
	
	// Use this for initialization
	void Start () {
	//aimode = "nuetral";
	}
	
	// Update is called once per frame
	void Update () {

	if(aimode != "dead")
		{
		if(aimode == "nuetral")
		{
		nuetralmove();	
		}
		
		if(aimode == "chase")
		{
		chasemove();
		}
		
		if(aimode == "isshooting")
		{
		shootprepare();	
		}
		
		if (aimode == "playingshoot")
		{
		playingshoot();
		}
		
		if (aimode == "stunned")
			{
			StartCoroutine(stunwait());
			}
		//check if is hit by player
		if(this.gameObject == grenade.hitenemy){thisdie();}
		if(this.gameObject == weapon.hitenemy){thisdie();}
		}
	if (aimode == "dead") { 		this.rigidbody.isKinematic = true;
	}
	}
}
