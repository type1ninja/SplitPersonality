using UnityEngine;
using System.Collections;

public class SelfMove : IndivMove {
	protected override Vector3 GetInput() {
		return new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
	}
	
	protected override bool GetJump() {
		return Input.GetButtonDown ("Jump");
	}
}