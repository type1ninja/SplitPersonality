using UnityEngine;
using System.Collections;

public class IndivMove : Photon.MonoBehaviour {

	CharacterController controller;

	float speed = 10.0f;
	float jumpSpeed = 6.0f;

	Vector3 moveDirection = Vector3.zero;

	bool hasJumped = false;

	void Start() {
		controller = GetComponentInParent<CharacterController>();
	}

	void FixedUpdate () {

		if (photonView.isMine) {
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
				photonView.RPC ("Jump", PhotonTargets.AllBuffered);
			} else {
				photonView.RPC ("DontJump", PhotonTargets.AllBuffered);
			}
		}
	}

	public Vector3 GetMoveDir() {
		return moveDirection;
	}

	public float GetUpSpeed() {
		return moveDirection.y;
	}

	private Vector3 GetInput() {
		return new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
	}

	private bool GetJump() {
		return Input.GetButtonDown ("Jump");
	}

	[PunRPC] void Jump() {
		moveDirection.y = jumpSpeed;
		hasJumped = true;
	}

	[PunRPC] void DontJump() {
		moveDirection.y = 0;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (moveDirection);
		} else {
			moveDirection = (Vector3) stream.ReceiveNext ();
		}
	}
}