using UnityEngine;
using System.Collections;

public class grenade : MonoBehaviour {
	public float blastradius;
	public float timedelay;
	public float raynum;
	public float spread;
	public GameObject particles;
	public GameObject splodeparticles;
	public AudioClip shotaudioclip;
	public LayerMask mask;
	public GameObject thechild;
	
	
	public static GameObject hitenemy = null;
	
	
	void OnCollisionEnter(Collision theobject) {
	if(theobject.gameObject.tag == "enemy")
		{
			explode();
			hitenemy = theobject.gameObject;
				
		}
	}
	
	void Destroy()
	{
	Debug.Log("isdestroyed");	
	}
	
	IEnumerator timebomb() {
	yield return new WaitForSeconds(timedelay);
	explode();
	}
	
	void explode()
	{
		thechild.animation.Stop ();
		thechild.animation.Play("explode");

	AudioSource.PlayClipAtPoint(shotaudioclip, transform.position);

	shootgrenaderays();
	GameObject instance = Instantiate(particles, transform.position, transform.rotation) as GameObject;
	StartCoroutine(stopexisting());
	}
	
	void shootgrenaderays()
	{	

	GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy");

		float whichi = .1F;


		for(int i = 0; i < enemy.Length; i++)
		{
			RaycastHit hiti;
			GameObject enemyi = enemy[i];
			
			float enemydist = Vector3.Distance(transform.position, enemyi.transform.position);
			if(enemydist <= blastradius)
				
			{
	            Debug.DrawLine(transform.position, enemyi.transform.position, Color.green, 2);
					if(!Physics.Linecast (transform.position, enemyi.transform.position, out hiti, mask))
				{
		            Debug.DrawLine(transform.position, hiti.point, Color.red, 2);
//					hitenemy = hiti.transform.gameObject;
				StartCoroutine(killallhit(enemyi, whichi));
				whichi = whichi + .1F;
				}
				}
		}
		//list of hit enemies

	}
	
	IEnumerator killallhit(GameObject enemy, float delay){
	yield return new WaitForSeconds(delay);
	hitenemy = enemy;
	}
	
	IEnumerator stopexisting(){
		
		rigidbody.isKinematic = true;
		Destroy(this.collider);

	yield return new WaitForSeconds(1.2F);
	GameObject instance = Instantiate(splodeparticles, transform.position, transform.rotation) as GameObject;
	Destroy(gameObject);
	}
	
		
	
	// Use this for initialization
	void Start () {
		StartCoroutine(timebomb());

	}
	
	// Update is called once per frame
	void Update () {

	}
}
