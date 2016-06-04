using UnityEngine;
using System.Collections;

public class GroupLook : MonoBehaviour {
	Transform players;
	
	//The players body, whose yaw is changed instead of changing the head's yaw
	Transform characterBody;
	
	void Start() {
		characterBody = transform.parent;
		players = transform.parent.Find ("Players");
	}

	void Update() {
		
		Vector3 newHeadRot = Vector3.zero;
		Vector3 newBodyRot = Vector3.zero;
		
		//TODO - only grab these when somebody joins (if that reduces lag)
		foreach (Transform child in players) {
			//Add up all the head rotations
			newHeadRot += new Vector3 (child.localEulerAngles.x, 0, child.localEulerAngles.z);
		}
		
		//TODO - only grab these when somebody joins (if that reduces lag)
		foreach (Transform child in players) {
			//Add up all the body rotations
			newBodyRot += new Vector3 (0, child.localEulerAngles.y, 0);
		}
		
		transform.localEulerAngles = newHeadRot;
		characterBody.localEulerAngles = newBodyRot;
	}
}
