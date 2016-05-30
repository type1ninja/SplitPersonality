using UnityEngine;
using System.Collections;

public class GroupLook : MonoBehaviour {
	//TODO -- make this an array
	Transform player1;
	Transform player2;

	//The players body, whose yaw is changed instead of changing the head's yaw
	Transform characterBody;

	void Start() {
		characterBody = transform.parent;

		player1 = transform.parent.Find ("Players").Find ("Player1");
		player2 = transform.parent.Find ("Players").Find ("Player2");
	}

	void Update() {
		Vector3 newHeadRot = Vector3.zero;
		Vector3 newBodyRot = Vector3.zero;

		//Add up all the head rotations
		newHeadRot += new Vector3 (player1.localEulerAngles.x, 0, player1.localEulerAngles.z);
		newHeadRot += new Vector3 (player2.localEulerAngles.x, 0, player2.localEulerAngles.z);

		//Add up all the body rotations
		newBodyRot += new Vector3 (0, player1.localEulerAngles.y, 0);
		newBodyRot += new Vector3 (0, player2.localEulerAngles.y, 0);

		transform.localEulerAngles = newHeadRot;
		characterBody.localEulerAngles = newBodyRot;
	}
}