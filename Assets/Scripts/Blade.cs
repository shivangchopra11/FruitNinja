using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

	public GameObject bladeTrailPrefab;
//	public float minCuttingVelocity = 0.001f;

	bool isCutting = false;

	Vector3 previousPosition;

	GameObject currentBladeTrail;

	Rigidbody rb;
	Camera cam;
//	CircleCollider2D circleCollider;

	void Start ()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();
//		circleCollider = GetComponent<CircleCollider2D>();
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

		rb.position = newPosition;

//		float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
//		if (velocity > minCuttingVelocity)
//		{
//			circleCollider.enabled = true;
//		} else
//		{
//			circleCollider.enabled = false;
//		}
//
		previousPosition = newPosition;
	}

	void StartCutting ()
	{
		isCutting = true;
		currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
		previousPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5f));
//		circleCollider.enabled = false;
	}

	void StopCutting ()
	{
		isCutting = false;
		currentBladeTrail.transform.SetParent(null);
//		Destroy(currentBladeTrail, 2f);
//		circleCollider.enabled = false;
	}

}