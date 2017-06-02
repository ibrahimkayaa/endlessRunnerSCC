using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	public enum GameState{
		MainMenu,
		Menu,
		InGame,
		Start,
		EndGame
	}

	public GameObject character;
	public List<Transform> activePadList = new List<Transform>();
	public List<Transform> nonActivePadList = new List<Transform>();
	public GameObject poolObject;
	public float speed;

	public GameState gmState = GameState.MainMenu;
	public float distanceScore;
	public int coinScore;

	public Text coinScoreText;
	public Text distanceScoreText;
	public Text highestScoreText;
	public Text bankText;
	public Text countDownText;

	public GameObject mainMenuPanel;
	public GameObject inGameHUD;


	//Private Variables
	Vector3 lastActivePadOffset;
	private int dynamiclyDifficultyAdjustmentMultiplier = 1;
	private int dynamiclyDifficultyMaxMultiplier = 6;
	private int dynamiclydifficultythreshold = 15;
	float speedAdjustment = 1f;
	private int highestScore = 0;
	private int bankMoney = 0;
	private float waitSec = 3f;

	void Start () {

		SpawnManager.Instance.Spawn ();

		if(PlayerPrefs.HasKey ("Highest_Score") == false){

			PlayerPrefs.SetInt ("Highest_Score",highestScore);
		}else{
			highestScore = PlayerPrefs.GetInt ("Highest_Score");
		}


		if(PlayerPrefs.HasKey ("Bank") == false){
			PlayerPrefs.SetInt ("Bank",bankMoney);
		}
		else{
			bankMoney = PlayerPrefs.GetInt ("Bank");
		}

		highestScoreText.text = highestScore.ToString () + "m";
		bankText.text = bankMoney.ToString () + "c";



		lastActivePadOffset = SpawnManager.Instance.lastActivePad.position - character.transform.position;

	}
	
	// Update is called once per frame
	void Update () {


		if(gmState == GameState.EndGame){
			if(Input.GetKeyDown (KeyCode.Space)){
				SceneManager.LoadScene (0);
			}
		}

		if(gmState == GameState.MainMenu){

			if(Input.GetKeyDown (KeyCode.Space)){

				mainMenuPanel.SetActive (false);
				inGameHUD.SetActive (true);
				gmState = GameState.Menu;
			}
		}


		if(gmState == GameState.Menu){
			
			waitSec -= Time.deltaTime;
			countDownText.text = Mathf.RoundToInt (waitSec).ToString () ;

		}


		if(waitSec <= 0 ){
			waitSec = 3f;
			countDownText.gameObject.SetActive (false);
			Debug.Log (waitSec);
			gmState = GameState.Start;

		}
		//GameManager.Instance.gmState = GameManager.GameState.InGame;

		if(poolObject !=null && gmState == GameState.Start){


			if(distanceScore >= dynamiclydifficultythreshold ){
				SetDifficultyLevel ();
				
			}

			poolObject.transform.position += Vector3.back * speed * Time.deltaTime;
			distanceScore += Time.deltaTime;

			string stringFormat = Mathf.RoundToInt (distanceScore).ToString () + "m";

			distanceScoreText.text = stringFormat;

			if(SpawnManager.Instance.lastActivePad.position.z - Camera.main.transform.position.z < lastActivePadOffset.z - (SpawnManager.Instance.padLength * 2f)){



				SpawnManager.Instance.RecyclePad ();
				SpawnManager.Instance.ReUsePad ();
			}
		}
		
	}

	void SetDifficultyLevel(){

		if (dynamiclyDifficultyAdjustmentMultiplier == dynamiclyDifficultyMaxMultiplier)
			return;

		dynamiclydifficultythreshold *= 2;
		dynamiclyDifficultyAdjustmentMultiplier++;
		speedAdjustment = (float)dynamiclyDifficultyAdjustmentMultiplier/3f;
		speed += speedAdjustment;
		Debug.Log (dynamiclyDifficultyAdjustmentMultiplier);
		Debug.Log (dynamiclydifficultythreshold);
		Debug.Log (speedAdjustment);
		Debug.Log (speed);
	}

	public void SetCoinScore(){
		coinScore++;
		string stringFormat = coinScore.ToString () +  "c";
		coinScoreText.text = stringFormat;
	}

	public void CheckAndSetHighestScore(){

		if(distanceScore > highestScore){

			PlayerPrefs.SetInt ("Highest_Score", (int)distanceScore);
			highestScoreText.text = highestScore.ToString () + "m";
		}

		bankMoney += coinScore;
		PlayerPrefs.SetInt ("Bank",bankMoney);
		bankText.text = bankMoney.ToString () + "c";
		inGameHUD.gameObject.SetActive (false);
		mainMenuPanel.SetActive (true);
		gmState = GameState.EndGame;

	}


}
