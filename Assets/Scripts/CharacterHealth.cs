using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {

	//Void damage per second
	float VOIDDMGPERSEC = -75;
	
	GroupMove move;

	float maxHealth = 100f;
	float health;
	//float healthRegen = 5.0f;

	void Start() {
		move = GetComponent<GroupMove> ();
		health = maxHealth;
	}

	void FixedUpdate() {

		//Void Death
		if (transform.position.y <= -4) {
			ModHealth(VOIDDMGPERSEC * Time.fixedDeltaTime);
		}

		if (health > maxHealth) {
			health = maxHealth;
		} else if (health < 0) {
			Die ();
		}

		//Regen, if you want
		//ModHealth(healthRegen * Time.fixedDeltaTime);
	}

	//Damage is inputted as a negative number
	//and Regen as a positive number
	public void ModHealth(float diff) {
		health += diff;
	}
	
	void Die() {
		transform.position = new Vector3 (0, .51f, 0);
		health = maxHealth;
		move.StopMotion ();

	}

	public float GetMaxHealth() {
		return maxHealth;
	}
	public float GetHealth() {
		return health;
	}
}