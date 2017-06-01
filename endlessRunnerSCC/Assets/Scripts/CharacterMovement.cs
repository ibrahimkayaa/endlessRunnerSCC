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


	void Awake () {

		animController = GetComponent <Animator> ();
		charBody = GetComponent <Rigidbody> ();
		charCapsul = GetComponent <CapsuleCollider> ();
		isMoving = false;
	}
	

	void Update () {
		charVelocity = Vector3.zero;

		groundCollidersArray = Physics.OverlapSphere (groundCheckPoint.position, groundCheckRadius, groundCheckLayerMask);

		isGrounded = (groundCollidersArray.Length > 0) ? true : false;


		if(Input.GetKeyDown (KeyCode.Space) && !isMoving){
			isMoving = true;
			animController.SetFloat ("Speed",speed);
		}

		if(Input.GetKeyDown (KeyCode.J) && isGrounded){



			charBody.AddForce (0f, jumpForce.y * Time.fixedDeltaTime, jumpForce.x * Time.fixedDeltaTime, ForceMode.Impulse);
			animController.SetTrigger ("Jump");
			inAir = true;

		}

		if(isMoving && isGrounded ){
			inAir = false;
			animController.SetTrigger ("Jump_Roll");
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
	}
}
