using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour {

	public GameObject target;

	public Vector3 offset;

	void Start() {


		target = GameObject.FindGameObjectWithTag ("Player") as GameObject;


		if(target !=null){

			offset = transform.position - target.transform.position;
		}else{
			Debug.LogWarning ("There is no Character in the scene");
		}


	}
	
	// Update is called once per frame
	void LateUpdate () {
		
	}
}
