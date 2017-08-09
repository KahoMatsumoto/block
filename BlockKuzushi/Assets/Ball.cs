using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed = 20.0f;    //これを追加

	// Use this for initialization
	void Start () {
		//以下を追加
		this.GetComponent<Rigidbody>().AddForce(
			(transform.forward + transform.right) * speed,
			ForceMode.VelocityChange);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
