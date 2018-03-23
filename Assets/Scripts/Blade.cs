using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blade : MonoBehaviour {

	public GameObject bladeTrailPrefab;
	public float minCuttingVelocity = 0.01f;

	bool isCutting = false;

	Vector3 previousPosition;

	GameObject currentBladeTrail;

	Rigidbody rb;
	Camera cam;
//	CircleCollider2D circleCollider;
	SphereCollider sphereCollider;



	public GameObject[] splashReference;
	private Vector3 randomPos;
	private Text scoreReference;
	int pos;

	void Start ()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();
		sphereCollider = GetComponent<SphereCollider>();
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




	}


	void UpdateCut ()
	{
		Vector3 newPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5f));
//		newPosition.z = 5f;
		rb.position = newPosition;

		float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
		if (velocity > minCuttingVelocity)
		{
			sphereCollider.enabled = true;
		} else
		{
			sphereCollider.enabled = false;
		}

		previousPosition = newPosition;
	}

	void StartCutting ()
	{
		isCutting = true;
		currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
		previousPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5f));
//		previousPosition.z = 5f;
		sphereCollider.enabled = false;
	}

	void StopCutting ()
	{
		isCutting = false;
		currentBladeTrail.transform.SetParent(null);
		Destroy(currentBladeTrail, 2f);
		sphereCollider.enabled = false;
	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log (other.gameObject.tag);
//		if(other.gameObject.tag == "Line")
//		{
//			//			Camera.main.GetComponent<AudioSource>().Play();
//			Destroy(gameObject);
//		}
		Destroy(other.gameObject);
		Instantiate(splashReference[pos], randomPos, transform.rotation);

		/* Update Score */

		scoreReference.text = (int.Parse(scoreReference.text) + 1).ToString();
	}

}