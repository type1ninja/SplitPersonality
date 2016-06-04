using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public Vector3 moveDirection = Vector3.zero;

	private float speed = 10.0f;
	
	void Update () {
		moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));

		//If the character is moving diagonally, divide magnitude by 1.4 to prevent huge speed buffs from diagonal walking
		if (moveDirection.x != 0 && moveDirection.z != 0) {
			moveDirection /= 1.4f;
		}

		moveDirection *= speed;
	}
}