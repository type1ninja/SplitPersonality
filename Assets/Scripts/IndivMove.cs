using UnityEngine;
using System.Collections;

public abstract class IndivMove : MonoBehaviour {

	CharacterController controller;

	float speed = 10.0f;
	float jumpSpeed = 6.0f;

	Vector3 moveDirection = Vector3.zero;

	bool hasJumped = false;

	void Start() {
		controller = GetComponentInParent<CharacterController>();
	}

	void FixedUpdate () {

		moveDirection = GetInput ();
		
		moveDirection *= speed;
		//If the character is moving diagonally, divide magnitude by 1.4 to prevent huge speed buffs from diagonal walking
		if (moveDirection.x != 0 && moveDirection.z != 0) {
			moveDirection /= 1.4f;
		}

		if (controller.isGrounded) {
			hasJumped = false;
		}

		if (GetJump () && !hasJumped) {
			moveDirection.y = jumpSpeed;
			hasJumped = true;
		} else {
			moveDirection.y = 0;
		}
	}

	public Vector3 GetMoveDir() {
		return moveDirection;
	}

	public float GetUpSpeed() {
		return moveDirection.y;
	}

	protected abstract Vector3 GetInput();

	protected abstract bool GetJump();
}