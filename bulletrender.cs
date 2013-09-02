using UnityEngine;
using System.Collections;

public class bulletrender : MonoBehaviour {

	
	public static Vector3 point1;
	public static Vector3 point2;
	
	public static float fadedelay = .05F;
	public static float laserwidth1 = .04F;
	public static float laserwidth2 = .04F;

	bool visiblelaser = false;
	
	public LineRenderer thisRenderer;
	
	
	void showlaser()
	{
	thisRenderer.SetPosition(0, point1);
	thisRenderer.SetPosition(1, point2);
	thisRenderer.SetWidth(laserwidth1, laserwidth2);
		
	StartCoroutine(delaykill());
	}
	
	IEnumerator delaykill()
	{
	yield return new WaitForSeconds(fadedelay);
	killlaser();
		
	}
	
	void killlaser()
	{
	thisRenderer.enabled = false;
	thisRenderer = null;
	Destroy(gameObject);
	}
		
		
	// Use this for initialization
	void Start () {
	visiblelaser = true;
	}
	
	// Update is called once per frame
	void Update () {
	if(visiblelaser)
		{
		thisRenderer.enabled = true;
		showlaser();
		}
	}
}
