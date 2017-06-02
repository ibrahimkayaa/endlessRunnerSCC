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
	void Update () {

		if(transform.position.z < target.transform.position.z + offset.z ){

			transform.position = Vector3.Lerp (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (transform.position.x, transform.position.y, target.transform.position.z + offset.z), Time.deltaTime * 5f);
		}
	}
}
