using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private float health;

	void Start () {
		health = 100f;
	}
	
	void Update () {
	
	}

	public void TakeDamage(float damage) {
		Debug.Log ("Enemy took " + damage + " damage.");
		health -= damage;
		Debug.Log ("Health is at: " + health);
		if (health <= 0.0f) {
			Die();
		}
	}
	
	public void Die() {
		Debug.Log ("Enemy is dead.");
		// Die.
		health = 100f;
		Debug.Log ("Health reset to " + health);
	}
}
