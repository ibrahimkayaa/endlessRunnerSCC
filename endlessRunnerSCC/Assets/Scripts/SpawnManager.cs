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
	public Transform lastActivePad;
	[HideInInspector]
	public float padLength =  10f;


	//Private Variables
	GameObject padHolder;
	int recycleIndex = 0;






	public void Spawn(){

		Shuffle (padList);

		//GameManager.Instance.gmState = GameManager.GameState.InGame;

		padHolder = new GameObject ("Pool");
		padHolder.transform.SetParent (transform);
		/*Transform tempStarterPad = Instantiate (starterPad,lastSpawnPosition,Quaternion.identity);
		tempStarterPad.SetParent (padHolder.transform);
		tempStarterPad.gameObject.SetActive (true);*/
		GameManager.Instance.character = Instantiate (characterObject, characterSpawnPosition, Quaternion.identity);


		for(int i = 1; i < padList.Count + 1 ; i ++){

			Vector3 spawnPosition = new Vector3 (0, 0, i);
			spawnPosition.z *= 10f;

			if(i -1 < activePadCount){
				
				Transform tempPad = Instantiate (padList [i - 1], spawnPosition, Quaternion.identity);
				tempPad.SetParent (padHolder.transform);
				lastSpawnPosition = tempPad.position;
				lastActivePad = tempPad;
				GameManager.Instance.activePadList.Add (tempPad);

			}else{

				Transform tempPad = Instantiate (padList [i - 1], lastSpawnPosition, Quaternion.identity);
				tempPad.SetParent ( padHolder.transform);
				GameManager.Instance.nonActivePadList.Add (tempPad);
				tempPad.gameObject.SetActive (false);


			}


		}

		GameManager.Instance.poolObject = padHolder;
	}

	public void RecyclePad(){

		if(recycleIndex < GameManager.Instance.activePadList.Count){
			lastSpawnPosition = GameManager.Instance.activePadList [recycleIndex].transform.position;
			GameManager.Instance.activePadList [recycleIndex].gameObject.SetActive (false);
			GameManager.Instance.nonActivePadList.Add (GameManager.Instance.activePadList[recycleIndex]);

			GameManager.Instance.activePadList.RemoveAt (recycleIndex);
		}

	}

	public void ReUsePad(){

		Shuffle (GameManager.Instance.nonActivePadList);
		Transform tempPad = GameManager.Instance.nonActivePadList [0];


		Vector3 spawnPosition = Vector3.zero;
		spawnPosition.z += lastActivePad.position.z + padLength;


		tempPad.position = spawnPosition;
		tempPad.gameObject.SetActive (true);

		lastSpawnPosition = tempPad.position;
		lastActivePad = tempPad;
		GameManager.Instance.nonActivePadList.Remove (tempPad);
		GameManager.Instance.activePadList.Add (tempPad);

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
