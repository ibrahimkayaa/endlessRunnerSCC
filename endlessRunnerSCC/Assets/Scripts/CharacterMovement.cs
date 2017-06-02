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


		if(Input.GetKeyDown (KeyCode.Space) && !isMoving){
			isMoving = true;
			animController.SetFloat ("Speed",speed);
		}



		Vector3 rPosition = charBody.position;
		rPosition.x = Mathf.MoveTowards (rPosition.x, horizontalMovement, Time.fixedDeltaTime * 5f);
		charBody.position = new Vector3 (Mathf.Clamp (rPosition.x, -1f, 1f), rPosition.y, rPosition.z);

		if(Input.GetKeyDown (KeyCode.A) && myTransform.position == currentPosition){


			horizontalMovement = charBody.position.x - 1f ;

		}else if (Input.GetKeyDown (KeyCode.D) && myTransform.position == currentPosition){


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

	void FixedUpdate(){

		if(isMoving){
			RunLoop ();
		}
	}

	void RunLoop(){


		charVelocity.z = speed;
	
		/*charBody.velocity = charVelocity * Time.fixedDeltaTime;*/
		transform.position += charVelocity * Time.fixedDeltaTime;
		currentPosition = transform.position;
	}
}
