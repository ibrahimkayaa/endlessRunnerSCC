using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	Transform bigParent;
	Transform grandPa;
	// Use this for initialization
	void Start () {

		bigParent = transform.parent.GetComponentInParent <Transform> ();
		grandPa = bigParent.parent.GetComponentInParent <Transform> ();
		grandPa.gameObject.GetComponent <Pad>().OwnCoinsList.Add (transform) ;

	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.up * 90f * Time.deltaTime);
	}

	void OnEnable(){

		GetComponent <Animator>().SetTrigger ("Spin");

	}
}
