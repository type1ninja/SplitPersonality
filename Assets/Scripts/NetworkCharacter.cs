using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {
	

	Quaternion realRot = Quaternion.identity;
	Vector3 realPos = Vector3.zero;
	
	void Update () {
		if (photonView.isMine) {
			//Do nothing, inputs handle this
		} else {
			transform.position = Vector3.Lerp (transform.position, realPos, 0.1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRot, 0.1f);
		}
	}
	
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			realPos = (Vector3) stream.ReceiveNext ();
			realRot = (Quaternion) stream.ReceiveNext ();
		}
	}
}