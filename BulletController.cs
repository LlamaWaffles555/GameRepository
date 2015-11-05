using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float damage;

	void Start () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			other.GetComponent<PlayerController> ().TakeDamage (damage);
		} else if (other.gameObject.CompareTag ("Enemy")) {
			other.GetComponent<EnemyController> ().TakeDamage (damage);
		}
	}
	
	void Update () {
	
	}
}
