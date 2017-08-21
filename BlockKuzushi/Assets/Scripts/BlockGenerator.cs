using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {

	public Transform blockPrefab;
	public float z;

	// Use this for initialization
	void Start () {
		//配置する座標を設定
		Vector3 placePosition = new Vector3(-15,0,z);
		//配置する回転角を設定
		Quaternion q = new Quaternion();
		q= Quaternion.identity;
		//配置
		Instantiate(blockPrefab,placePosition,q);

		for (int i = 0; i < 15; i++) {
			//x座標を変更し配置
			placePosition.x += blockPrefab.transform.localScale.x;
			Instantiate(blockPrefab, placePosition, q);
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
