using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterMovement : MonoBehaviour {

	//Public Variables
	public float speed;
	public bool isMoving;

	//Private Variables
	Animator animController;
	Rigidbody charBody;
	CapsuleCollider charCapsul;
	Vector3 charVelocity;



	void Awake () {

		animController = GetComponent <Animator> ();
		charBody = GetComponent <Rigidbody> ();
		charCapsul = GetComponent <CapsuleCollider> ();
		isMoving = false;
	}
	

	void Update () {

		if(Input.GetKeyDown (KeyCode.Space) && !isMoving){
			isMoving = true;
			animController.SetFloat ("Speed",speed);
		}



	}

	void FixedUpdate(){

		if(isMoving){
			RunLoop ();
		}
	}

	void RunLoop(){

		charVelocity = Vector3.zero;
		charVelocity.z = speed;
		/*charBody.velocity = charVelocity * Time.fixedDeltaTime;*/
		transform.position += charVelocity * Time.fixedDeltaTime;
	}
}
