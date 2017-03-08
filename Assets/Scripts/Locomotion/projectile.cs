using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {


	public float lifeTime;
	protected float timeAlive;

	// Use this for initialization
	void Start () {
		timeAlive = lifeTime;
	}
	
	// Update is called once per frame
	void Update () {
		timeAlive -= Time.deltaTime;
		if (timeAlive < 0.0f) {
			Destroy (gameObject);
		}
	}
}
