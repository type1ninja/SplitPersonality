using UnityEngine;
using System.Collections;

public class OtherMove : IndivMove {

	public Vector3 moveDir = Vector3.zero;

	protected override Vector3 GetInput() {
		return moveDir;
	}
	
	protected override bool GetJump() {
		return false;
	}
}