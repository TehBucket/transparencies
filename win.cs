using UnityEngine;
using System.Collections;

public class win : MonoBehaviour {
	public static int playerhealth = 1;
	
	public AudioClip winsound;
	public AudioClip losesound;
	public GameObject dieparticles;
		
	GameObject playerpoint;
	
	//beardless ai, not used
//	public static void TakeDamage(float damage)
//	{
//	playerhealth--;	
//	}
	

	
	// Use this for initialization
	void Start () {
	playerhealth = 1;
    Screen.lockCursor = true;
	}
	
	
	IEnumerator playerdie()
	{
	yield return new WaitForSeconds(.04F);
	Application.LoadLevel(Application.loadedLevel);
	}
	// Update is called once per frame
	void Update () {
	GameObject[] enemiesleft = GameObject.FindGameObjectsWithTag("enemy") as GameObject[];

		
	if(playerhealth == 0)
		{
			playerhealth = 1;
			playerpoint = GameObject.FindWithTag("MainCamera");
		AudioSource.PlayClipAtPoint(losesound, playerpoint.transform.position);
		GameObject tmpdieparticles = Instantiate(dieparticles, playerpoint.transform.position, playerpoint.transform.rotation) as GameObject;
		StartCoroutine(playerdie());
		}
		
	if(enemiesleft.Length == 0)
		{
		playerpoint = GameObject.FindWithTag("MainCamera");
		AudioSource.PlayClipAtPoint(winsound, playerpoint.transform.position);
		Debug.Log ("won, loading next level");
		Application.LoadLevel(Application.loadedLevel + 1);
		}
		if(Input.GetKeyDown(KeyCode.F6))
		{
					Application.LoadLevel(Application.loadedLevel + 1);
		}
		
					if(Input.GetKeyDown (KeyCode.Escape))
			{
			MouseLook.themenu = !MouseLook.themenu;
			}
		if (MouseLook.themenu == false)
		{
		Screen.lockCursor = true;
		Time.timeScale = 1;
		}
		
	}
}
