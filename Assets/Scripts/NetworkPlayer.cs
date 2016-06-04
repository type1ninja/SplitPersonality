using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour {

	public PlayerControl pc;

	Quaternion realRot = Quaternion.identity;
	Vector3 realMoveDir = Vector3.zero;

	void Update () {
		if (photonView.isMine) {
			//Do nothing, inputs handle this
		} else {
			transform.rotation = Quaternion.Lerp (transform.rotation, realRot, 0.1f);
			pc.moveDirection = Vector3.Lerp (pc.moveDirection, realMoveDir, 0.1f);
		}
	}
	
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (transform.rotation);
			stream.SendNext (pc.moveDirection);
		} else {
			realRot = (Quaternion) stream.ReceiveNext ();
			realMoveDir = (Vector3) stream.ReceiveNext ();
		}
	}
}