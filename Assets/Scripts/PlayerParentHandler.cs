using UnityEngine;
using System.Collections;

public class PlayerParentHandler : Photon.MonoBehaviour {

	[PunRPC] void SetParentToChar() {
		transform.parent = GameObject.Find ("Character").transform.Find ("Players");
	}
}