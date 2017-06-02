using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour {

	// Use this for initialization

	public List<Transform> OwnCoinsList = new List<Transform>();


	void Start () {



	}
	

	void OnDisable(){

		if(OwnCoinsList.Count > 0){

			for(int i = 0 ; i < OwnCoinsList.Count ; i ++){

				if(OwnCoinsList[i].gameObject.activeSelf == false){
					OwnCoinsList [i].gameObject.SetActive (true);
				}
			}
		}


	}

}
