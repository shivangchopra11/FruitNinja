using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour {

//	private GameObject gameObject;
//	[SerializeField]
	public GameObject[] splashReference;
	private Vector3 randomPos;
	private Text scoreReference;
	int pos;

	// Use this for initialization
	void Start () {
//		gameObject = GetComponent<Rigidbody>();
		pos = Random.Range (0, 3);
		scoreReference = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
		randomPos = new Vector3(Random.Range(-1, 1), Random.Range(0.3f, 0.7f), Random.Range(-6.5f, -7.5f));
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -30)
		{
			Destroy(gameObject);
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name == "Line")
		{
			Camera.main.GetComponent<AudioSource>().Play();
			Destroy(gameObject);

			Instantiate(splashReference[pos], randomPos, transform.rotation);

			/* Update Score */

			scoreReference.text = (int.Parse(scoreReference.text) + 1).ToString();
		}
	}
}
