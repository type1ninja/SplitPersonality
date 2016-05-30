using UnityEngine;
using System.Collections;

//Taken from the Unity3D documention
//http://docs.unity3d.com/ScriptReference/CharacterController.Move.html
//Some slight modifications were made for SplitPersonality
public class GroupMove : MonoBehaviour {

	CharacterController controller;

	IndivMove player1Move;
	IndivMove player2Move;
	
	float gravity = Physics.gravity.y;

	Vector3 moveDirection = Vector3.zero;
	float upSpeed = 0;

	bool canMove = true;

	void Start() {
		controller = GetComponent<CharacterController>();

		player1Move = transform.Find ("Players").Find ("Player1").GetComponent<IndivMove> ();
		player2Move = transform.Find ("Players").Find ("Player2").GetComponent<IndivMove> ();
	}

	void FixedUpdate() {
		if (canMove) {
			if (controller.isGrounded) {
				moveDirection = GetInput ();
				moveDirection = transform.TransformDirection (moveDirection);
			}
		}

		if (GetInput ().y != 0) {
			upSpeed = GetInput ().y;

			if (!controller.isGrounded) {
				moveDirection = GetInput ();
				moveDirection = transform.TransformDirection (moveDirection);
			}
		}

		//ADD gravity because it's negative
		upSpeed += gravity * Time.fixedDeltaTime;

		moveDirection.y = upSpeed;

		controller.Move(moveDirection * Time.deltaTime);
	}

	public void StopMotion() {
		moveDirection = Vector3.zero;
	}

	//Enemies can add knockback by basically adding a vector to the player's move, then lifting them into the air
	//Then, since the player can't move around in midair, they've taken knockback
	//Force isn't really a force, it's more like a new move direction
	public void AddKnockback(Vector3 force) {
		controller.Move(new Vector3(0, .03f, 0));
		//add force to the motion
		moveDirection += force;
	}

	private Vector3 GetInput() { 
		return player1Move.GetMoveDir () + player2Move.GetMoveDir ();
	}

	private float GetJumpInput() {
		return player1Move.GetUpSpeed () + player2Move.GetUpSpeed ();
	}
}