using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour {

	//public float accel = 1000.0f;//加える力の大きさ

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
//		//力を加える
//		this.GetComponent<Rigidbody>().AddForce(
//			transform.right*Input.GetAxisRaw("Horizontal")*accel,
//			ForceMode.Impulse);

		//マウスのx,y座標を取得
		Vector3 vecMouse = Input.mousePosition;
		vecMouse.z = -6;
//		Debug.Log (vecMouse);
		//ワールド座標に変換じゃ！！
		Vector3 screenPos = Camera.main.ScreenToWorldPoint(vecMouse);
//		Debug.Log (screenPos);
		//オブジェクトに代入じゃ！！
		Vector3 racketPos = new Vector3(screenPos.x*-1, 0, -6);
//		Debug.Log (racketPos);
		transform.position = racketPos;
	}
}
