using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> {

	//Public Variables
	public List<Transform> padList = new List<Transform>();
	public Transform starterPad;
	public Vector3 lastSpawnPosition = Vector3.zero;
	public Vector3 characterSpawnPosition;
	public GameObject characterObject;
	public int activePadCount;



	//Private Variables
	GameObject padHolder;
	GameObject character;


	public void Spawn(){

		Shuffle (padList);

		padHolder = new GameObject ("Pool");
		padHolder.transform.SetParent (transform);
		Transform tempStarterPad = Instantiate (starterPad,lastSpawnPosition,Quaternion.identity);
		tempStarterPad.SetParent (padHolder.transform);
		character = Instantiate (characterObject, characterSpawnPosition, Quaternion.identity);


		for(int i = 1; i < padList.Count + 1 ; i ++){

			Vector3 spawnPosition = new Vector3 (0, 0, i);
			spawnPosition.z *= 10f;

			if(i -1 < activePadCount){
				
				Transform tempPad = Instantiate (padList [i - 1], spawnPosition, Quaternion.identity);
				tempPad.SetParent (padHolder.transform);
				lastSpawnPosition = tempPad.position;
				GameManager.Instance.activePadList.Add (tempPad);

			}else{

				Transform tempPad = Instantiate (padList [i - 1], lastSpawnPosition, Quaternion.identity);
				tempPad.SetParent ( padHolder.transform);
				GameManager.Instance.nonActivePadList.Add (tempPad);
				tempPad.gameObject.SetActive (false);


			}


		}
	}

	public void RecyclePad(){
		
	}

	public void ReUsePad(){
		
	}

	private void Shuffle(List<Transform> tArray){

		int n = tArray.Count;

		while (n > 0) {
			n--;
			int k = Random.Range (0, n + 1);
			Transform tempTransform = tArray [k];
			tArray [k] = tArray [n];
			tArray [n] = tempTransform;

		}
	}

}
