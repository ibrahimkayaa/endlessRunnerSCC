using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterMovement : MonoBehaviour {

	//Public Variables
	public float speed;
	public Vector2 jumpForce;
	public bool isMoving;
	public bool isGrounded;
	public bool inAir;
	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask groundCheckLayerMask;



	//Private Variables
	Animator animController;
	Rigidbody charBody;
	CapsuleCollider charCapsul;
	Vector3 charVelocity;
	Collider[] groundCollidersArray;
	float horizontalMovement;
	Vector3 currentPosition;
	Transform myTransform;




	void Awake () {

		animController = GetComponent <Animator> ();
		charBody = GetComponent <Rigidbody> ();
		charCapsul = GetComponent <CapsuleCollider> ();
		isMoving = false;
		currentPosition = transform.position;
		myTransform = transform;
		horizontalMovement = charBody.position.x;
	}
	

	void Update () {




		charVelocity = Vector3.zero;

		groundCollidersArray = Physics.OverlapSphere (groundCheckPoint.position, groundCheckRadius, groundCheckLayerMask);

		isGrounded = (groundCollidersArray.Length > 0) ? true : false;


		if (/*Input.GetKeyDown (KeyCode.Space) && */!isMoving && GameManager.Instance.gmState == GameManager.GameState.Start) {
			isMoving = true;
			//GameManager.Instance.gmState = GameManager.GameState.Start;
			animController.SetFloat ("Speed",speed);
		}



		Vector3 rPosition = charBody.position;
		rPosition.x = Mathf.MoveTowards (rPosition.x, horizontalMovement, Time.fixedDeltaTime * 5f);
		charBody.position = new Vector3 (Mathf.Clamp (rPosition.x, -1f, 1f), rPosition.y, rPosition.z);


		if(GameManager.Instance.gmState == GameManager.GameState.Start){

			if (Input.GetKeyDown (KeyCode.A)) {


				horizontalMovement = charBody.position.x - 1f ;

			} else if (Input.GetKeyDown (KeyCode.D)) {


				horizontalMovement = charBody.position.x +1f;
			}

			Vector3 targetPosition = new Vector3 (Mathf.Clamp (currentPosition.x,-1f,1f) , transform.position.y, transform.position.z);




			if(isGrounded && isMoving){


				if(Input.GetKeyDown (KeyCode.J)){



					charBody.AddForce (0f, jumpForce.y * Time.fixedDeltaTime, jumpForce.x * Time.fixedDeltaTime, ForceMode.Impulse);

					animController.SetTrigger ("Jump");
					inAir = true;

					if(isMoving ){

						animController.SetTrigger ("Jump_Roll");
					}

				}
			
		}


		}
			


	}

	void FixedUpdate(){

		if(isMoving){
			RunLoop ();
		}
	}

	void RunLoop(){


		charVelocity.z = speed;


		//Changed my mind, don´t want to move forward, Character will be staying and playing animation . Entire Level will move towards to the camera and the character
		//It takes a while to implement various ways but did not like them very mucj .. I think this solution is the best and the common one of course. 
		//I have not done this way before so cleaning up to code was take a bit time to accomplish with.

		/*charBody.velocity = charVelocity * Time.fixedDeltaTime;*/
		/*transform.position += charVelocity * Time.fixedDeltaTime;*/
		/*transform.position = Vector3.MoveTowards (transform.position, charVelocity, Time.fixedDeltaTime * 2f);*/

		currentPosition = transform.position;
	}

	void OnCollisionEnter(Collision other){

		if(other.gameObject.tag == "Obstacle"){

			Vector3 contactVector = other.contacts [0].normal;

			bool frontHit = (contactVector == Vector3.back) ? true : false;

			if(frontHit){

				GameManager.Instance.gmState = GameManager.GameState.MainMenu;
				GameManager.Instance.CheckAndSetHighestScore ();
				animController.SetTrigger ("Death");
			}

		

		}

	}

	void OnTriggerEnter(Collider other){

		if(other.gameObject.tag == "Coin"){

			GameManager.Instance.SetCoinScore ();
			other.gameObject.SetActive (false);
		}
	}
}
