using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerController : MonoBehaviour {

	[HideInInspector] public bool inMenu;
	public GameObject mainCamera;
	private MainMenuController menu;
	public GameObject primaryWeapon;
	public GameObject secondaryWeapon;
	public WeaponController gun;

	public GameObject hud;

	public float health;
	public float lookSpeed;
	public float moveSpeed;
	public float sprintSpeedModifier;
	public float jumpHeight;

	// Called when the Object is enabled
	void OnEnable () {
	}

	// Called when the Object is disabled or destroyed
	void OnDisable () {

	}

	// Called before Start ()
	void Awake () {

	}

	// Use this for initialization
	void Start () {
		menu = MainMenuController.menu;
		if (primaryWeapon != null) {
			gun = primaryWeapon.GetComponent<WeaponController> ();
		} else if (secondaryWeapon != null) {
			gun = secondaryWeapon.GetComponent<WeaponController> ();
		} else {
			// If no weapon is selected
		}
		inMenu = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		health = 100f;
		lookSpeed = 5f;
		moveSpeed = 5f;
		sprintSpeedModifier = 1.5f;
		jumpHeight = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!inMenu) {
			//-----------------------------------CAMERA------------------------------------------//
			float hRot = Input.GetAxis ("Mouse X");
			float vRot = -Input.GetAxis ("Mouse Y");
			if ((mainCamera.GetComponent<Transform> ().localEulerAngles.x < 90f) || (mainCamera.GetComponent<Transform> ().localEulerAngles.x > 270f) || (mainCamera.GetComponent<Transform> ().localEulerAngles.x >= 90f && mainCamera.GetComponent<Transform> ().localEulerAngles.x <= 180f && vRot < 0f) || (mainCamera.GetComponent<Transform> ().localEulerAngles.x >= 180f && mainCamera.GetComponent<Transform> ().localEulerAngles.x <= 270f && vRot > 0f)) { // Locks the camera angle to 90 degrees up and 90 degrees down
				mainCamera.GetComponent<Transform> ().Rotate (vRot * lookSpeed, 0, 0); // Rotates the camera up and down
			} else {
				mainCamera.GetComponent<Transform> ().localEulerAngles = new Vector3 (mainCamera.GetComponent<Transform> ().localEulerAngles.x, 0, 0);
			}
			this.gameObject.GetComponent<Transform> ().Rotate (0, hRot * lookSpeed, 0); // Rotates the player left and right
			//mainCamera.GetComponent<Transform>().localEulerAngles = new Vector3(Mathf.Clamp(mainCamera.GetComponent<Transform>().localEulerAngles.x, 90f, 270f), 0, 0);
			//----------------------------------MOVEMENT--------------------------------------//
			// Inputs
			float hMove = Input.GetAxis ("Horizontal");
			float vMove = Input.GetAxis ("Vertical");
			bool jump = Input.GetKeyDown (KeyCode.Space);

			Vector3 movement = new Vector3 (hMove, 0, vMove);
			movement = this.gameObject.GetComponent<Transform> ().localRotation * movement;
			if (movement.magnitude > 1f) {
				movement.Normalize ();
			}
			movement += Vector3.up * this.gameObject.GetComponent<Rigidbody>().velocity.y;
			//------------------------------------------SPRINTING------------------------------------------------------//
			movement *= moveSpeed;
			if (Input.GetKey(KeyCode.LeftShift)) {
					movement *= sprintSpeedModifier;
			}
			//-----------------------------------JUMPING----------------------------------------------//

			RaycastHit hitInfo;
			if (Physics.Raycast (transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.1f)) {
				if (jump) {
					movement += Vector3.up * jumpHeight;
				}
			}

			//-------------------------------------FLYING-----------------------------------------------//
			/*
			if (Input.GetKey (KeyCode.Space) && Input.GetKey (KeyCode.LeftShift)) {
				movement.y = 0f;
			} else if (Input.GetKey (KeyCode.Space)) {
				movement += Vector3.up;
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				movement += Vector3.down;
			}
			this.gameObject.GetComponent<Rigidbody> ().velocity = movement + new Vector3 (0, this.gameObject.GetComponent<Rigidbody> ().velocity.y, 0);
			*/
			//--------------------------------------------MOVEMENT--------------------------------------------------//
			this.gameObject.GetComponent<Rigidbody> ().velocity = movement;
			//--------------------------------------------SHOOTING---------------------------------------------------//
			if (gun != null) {
				if (Input.GetMouseButtonDown(0))
				{
					gun.OnClick();
				} else if (Input.GetMouseButtonUp(0))
				{
					gun.OnStopClick();
				} else if (Input.GetMouseButton(0))
				{
					gun.OnHoldClick();
				}
				if (Input.GetKeyDown(KeyCode.R))
				{
					gun.ReloadFunction();
				}
			}
		}
		//----------------------------------------MENU-----------------------------------------------------//
		if (Input.GetKeyDown(KeyCode.P)) {
			if (menu != null && hud != null && menu.isVisible) {
				menu.isVisible = false;
				menu.UpdateVisibility();
				Cursor.lockState = CursorLockMode.Locked;
				//Cursor.visible = false;
				hud.SetActive(true);
				inMenu = false;
			} else if (menu != null && hud != null && !menu.isVisible) {
				menu.isVisible = true;
				menu.UpdateVisibility ();
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				hud.SetActive(false);
				inMenu = true;
			}
		}
	}
	
	void FixedUpdate () {
		
	}

	public void TakeDamage(float damage) {
		Debug.Log ("Player took " + damage + " damage.");
		health -= damage;
		Debug.Log ("Health is at: " + health);
		if (health <= 0.0f) {
			Die();
		}
	}

	public void Die() {
		Debug.Log ("Player is dead.");
		// Die.
		health = 100f;
		Debug.Log ("Health reset to " + health);
	}

	public void Load () {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData) bf.Deserialize(file);
			file.Close();
			// Retreive things from PlayerData HERE
		}
	}

	public void Save () {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();
		// Put things in PlayerData HERE
		bf.Serialize (file, data);
		file.Close ();
	}
}

[Serializable]
class PlayerData {

}
