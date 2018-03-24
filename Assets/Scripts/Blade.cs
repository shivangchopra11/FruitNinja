using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Blade : MonoBehaviour {

	public GameObject bladeTrailPrefab;
	public float minCuttingVelocity = 0.001f;

	bool isCutting = false;

	Vector3 previousPosition;

	GameObject currentBladeTrail;

	Rigidbody rb;
	Camera cam;
	public Text gameover;
//	CircleCollider2D circleCollider;
//	SphereCollider sphereCollider;
	public GameObject[] leftCut;
	public GameObject[] rightCut;


	public GameObject[] splashReference;
	private Vector3 randomPos;
	private Text scoreReference;
	int pos;

	void Start ()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();
//		sphereCollider = GetComponent<SphereCollider>();
		pos = Random.Range (0, 3);
		scoreReference = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
		randomPos = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-4.5f, 3.5f), 5f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Moved) {
				StartCutting ();
			}

			if (touch.phase == TouchPhase.Ended) {
				StopCutting ();
			}
			if (isCutting) {
				UpdateCut ();
			}

		}
		if (Input.GetMouseButtonDown(0))
		{
			StartCutting();
		} else if (Input.GetMouseButtonUp(0))
		{
			StopCutting();
		}

		if (isCutting)
		{
			UpdateCut();
		}

//		if (gameover.enabled == true) {
//			if (Input.touchCount > 0 || Input.GetMouseButtonDown (0)) {
//				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//			}
//		}




	}


	void UpdateCut ()
	{
		Vector3 newPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5f));
		newPosition.z = 1f;
//		newPosition.z = 5f;
		rb.position = newPosition;

//		float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
//		if (velocity > minCuttingVelocity)
//		{
//			sphereCollider.enabled = true;
//		} else
//		{
//			sphereCollider.enabled = false;
//		}

		previousPosition = newPosition;
	}

	void StartCutting ()
	{
		isCutting = true;
		currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
		previousPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5f));
		Debug.Log ("Blade"+previousPosition.z.ToString());
//		previousPosition.z = 1f;
//		previousPosition.z = 5f;
//		sphereCollider.enabled = false;
	}

	void StopCutting ()
	{
		isCutting = false;
		currentBladeTrail.transform.SetParent(null);
		Destroy(currentBladeTrail, 2f);
//		sphereCollider.enabled = false;
	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("COLLIDE" + other.gameObject.tag);
//		if(other.gameObject.tag == "Line")
//		{
//			//			Camera.main.GetComponent<AudioSource>().Play();
//			Destroy(gameObject);
//		}
		Vector3 temp1 = new Vector3(other.transform.position.x,other.transform.position.y,other.transform.position.z);
		int cutpos;
		if (other.gameObject.tag == "Apple") {
			cutpos = 0;
		} else if (other.gameObject.tag == "Kiwi") {
			cutpos = 1;
		} else if (other.gameObject.tag == "Strawberry") {
			cutpos = 2;
		} else {
			FruitSpawner.instance.CancelInvoke ("SpawnFruit");
			gameover.GetComponent<Text>().enabled = true;
//			Destroy(other.gameObject);
			other.rigidbody.freezeRotation = true;
			other.rigidbody.useGravity = false;
			Camera.main.GetComponent<AudioSource>().Pause();
			other.gameObject.GetComponent<AudioSource>().Play ();
			return;
		}
		GameObject left = Instantiate (leftCut [cutpos], temp1, leftCut [0].transform.rotation);
		GameObject right = Instantiate (rightCut [cutpos], temp1, rightCut [0].transform.rotation);
		Vector3 throwForceleft = new Vector3(-1 ,0, 0);
		Vector3 throwForceright = new Vector3(1 ,0, 0);
		if (cutpos != 2) {
			throwForceleft = new Vector3(-1 ,0, 0);
			throwForceright = new Vector3(1 ,0, 0);
		} else {
			throwForceleft = new Vector3(1 ,0, 0);
			throwForceright = new Vector3(-1 ,0, 0);
		}

		left.GetComponent<Rigidbody>().AddForce (throwForceleft, ForceMode.VelocityChange);
		right.GetComponent<Rigidbody>().AddForce (throwForceright, ForceMode.VelocityChange);
		Destroy(other.gameObject);
		GetComponent<AudioSource> ().Play ();
		pos = Random.Range (0, 3);
		randomPos = new Vector3(temp1.x,temp1.y, 5f);
		GameObject splash = Instantiate(splashReference[pos], randomPos, transform.rotation);
		Destroy (splash, 1f);

		/* Update Score */

		scoreReference.text = (int.Parse(scoreReference.text) + 1).ToString();
	}

}