using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	public List<Transform> activePadList = new List<Transform>();
	public List<Transform> nonActivePadList = new List<Transform>();

	void Start () {

		SpawnManager.Instance.Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
