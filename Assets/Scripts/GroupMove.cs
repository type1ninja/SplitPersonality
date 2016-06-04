using UnityEngine;
using System.Collections;

public class GroupMove : MonoBehaviour {

	CharacterController controller;
	PlayerControl[] players; 

	Vector3 moveDir = Vector3.zero;

	void Start() {
		controller = GetComponent<CharacterController>();
	}

	void FixedUpdate() {
		//Only call this when a new player joines (if that reduces lag)
		players = GetComponentsInChildren<PlayerControl> ();

		moveDir = Vector3.zero;
		foreach (PlayerControl player in players) {
			moveDir += player.moveDirection;
		}

		moveDir = transform.TransformDirection (moveDir);

		controller.Move (moveDir * Time.fixedDeltaTime);
	}
}