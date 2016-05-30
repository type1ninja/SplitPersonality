using UnityEngine;
using System.Collections;

//Taken from the Unity3D documention
//http://docs.unity3d.com/ScriptReference/CharacterController.Move.html
//Some slight modifications were made for SplitPersonality
public class GroupMove : MonoBehaviour {

	CharacterController controller;

	IndivMove[] playerMoves;
	
	float gravity = Physics.gravity.y;

	Vector3 moveDirection = Vector3.zero;
	float upSpeed = 0;

	void Start() {
		controller = GetComponent<CharacterController>();
	}

	void FixedUpdate() {
		//TODO - only grab these when somebody joins
		playerMoves = GetComponentsInChildren<IndivMove> ();

		if (controller.isGrounded) {
			moveDirection = GetInput ();
			moveDirection = transform.TransformDirection (moveDirection);
		}

		if (GetInput ().y != 0) {
			upSpeed = GetInput ().y;

			if (!controller.isGrounded) {
				moveDirection = GetInput ();
				moveDirection = transform.TransformDirection (moveDirection);
			}
		}

		if (!controller.isGrounded && PhotonNetwork.inRoom) {
			//ADD gravity because it's negative
			upSpeed += gravity * Time.fixedDeltaTime;
		}

		moveDirection.y = upSpeed;

		if (PhotonNetwork.inRoom) {
			controller.Move (moveDirection * Time.fixedDeltaTime);
		}
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
		Vector3 moveSum = Vector3.zero;

		foreach (IndivMove move in playerMoves) {
			moveSum += move.GetMoveDir ();
		}

		return moveSum;
	}

	private float GetJumpInput() {
		float upSpeedSum = 0;

		foreach (IndivMove move in playerMoves) {
			upSpeedSum += move.GetUpSpeed ();
		}

		return upSpeedSum;
	}
}