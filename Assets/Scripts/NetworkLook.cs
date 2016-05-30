using UnityEngine;
using System.Collections;

public class NetworkLook : Photon.MonoBehaviour {

	Quaternion realRot = Quaternion.identity;
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			//Do nothing, inputs handle this
		} else {
			transform.rotation = Quaternion.Lerp (transform.rotation, realRot, 0.1f);
		}
	}

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (transform.rotation);
		} else {
			realRot = (Quaternion) stream.ReceiveNext ();
		}
	}
}