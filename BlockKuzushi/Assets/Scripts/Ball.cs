using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed = 20.0f;    //これを追加
	public GameObject target;
	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		//以下を追加
		this.GetComponent<Rigidbody>().AddForce(
			(transform.forward + transform.right) * speed,
			ForceMode.VelocityChange);
		this.rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		

	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "block") {
			Vector3 heading = target.transform.position - transform.position;
			float distance = heading.magnitude;
			Vector3 direction = heading / distance; 
//			this.GetComponent<Rigidbody>().AddForce(
//				direction * speed,ForceMode.VelocityChange);
			this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.AddForce(direction*speed,ForceMode.VelocityChange);
		}
	}
}
