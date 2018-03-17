using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] fruits;
	private Vector3 throwForce = new Vector3(0, 13, 0);
	// Use this for initialization
	void Start()
	{
		InvokeRepeating("SpawnFruit", 2f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnFruit()
	{
		for (byte i = 0; i < 4; i++)
		{
			int pos = Random.Range (0, 3);
			var randomRotation = Quaternion.Euler( Random.Range(0, 360) , Random.Range(0, 360) , Random.Range(0, 360));
			GameObject fruit = Instantiate(fruits[pos], new Vector3(Random.Range(-7.0f, 7.0f), -5.0f, 0), randomRotation) as GameObject;
			fruit.GetComponent<Rigidbody>().AddForce (throwForce, ForceMode.VelocityChange);
		}
	}
}
