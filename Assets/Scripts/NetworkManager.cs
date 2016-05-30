using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Photon Unity Networking Code from quill18's Multiplayer FPS Tutorial
//https://www.youtube.com/watch?v=AIgwZK151-A
public class NetworkManager : MonoBehaviour {

	static string version = "SplitPersonality 0.0.0";
	static string defaultRoomName = "Default Room";

	Text connectionStatus; 

	Transform myPlayer;

	void Start () {
		connectionStatus = GameObject.Find ("ConnectionStatus").GetComponent<Text>();
		
		Connect ();
	}

	void Update() {
		connectionStatus.text = PhotonNetwork.connectionStateDetailed.ToString ();
	}
	
	void Connect() {
		PhotonNetwork.ConnectUsingSettings (version);
	}

	void SpawnMyPlayer() {

		myPlayer = PhotonNetwork.Instantiate ("Player", Vector3.zero, Quaternion.Euler (Vector3.zero), 0).transform;
		myPlayer.GetComponent<PhotonView> ().RPC ("SetParentToChar", PhotonTargets.AllBuffered);

		myPlayer.GetComponent<IndivMove> ().enabled = true;
		myPlayer.GetComponent<SimpleSmoothMouseLook> ().enabled = true;

		//TODO - instead of using AllBuffered RPC movements, get the current state from the master client here
	}

	//Sequential connecting stuff
	void OnConnectedToMaster() {
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom (defaultRoomName);
	}

	void OnJoinedRoom() {
		SpawnMyPlayer ();
	}
}