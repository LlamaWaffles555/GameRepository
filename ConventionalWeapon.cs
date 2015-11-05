using UnityEngine;
using System.Collections;

public class ConventionalWeapon : WeaponController {

	public GameObject bullet;

	private float fireRateTimer;
	private bool reloading;
	private int currClipSize;
	
	private bool automatic;
	private float damage;
	private float fireRate; // seconds between shots
	private int clipSize;
	private float reloadSpeed; // in seconds
	private float bulletVelocity;

	public ConventionalWeapon () : this (true, 10f, 0.5f, 15, 2f, 100f) {}

	public ConventionalWeapon (float dmg, float fireRate, int clip, float reload, float velocity) : this (true, dmg, fireRate, clip, reload, velocity) {}

	public ConventionalWeapon (bool auto, float dmg, float fireRate, int clip, float reload) : this (auto, dmg, fireRate, clip, reload, 100f) {}

	public ConventionalWeapon (float dmg, float fireRate, int clip, float reload) : this (true, dmg, fireRate, clip, reload, 100f) {}

	public ConventionalWeapon (bool auto, float dmg, float fireRate, int clip, float reload, float velocity)
	{
		this.automatic = auto;
		this.damage = dmg;
		this.fireRate = fireRate;
		this.clipSize = clip;
		this.reloadSpeed = reload;
		this.bulletVelocity = velocity;
	}

	void Start () {
		currClipSize = clipSize;
		fireRateTimer = 0f;
		reloading = false;
	}
	
	void Update () {
		if (fireRateTimer > 0)
		{
			fireRateTimer -= Time.deltaTime;
		}
	}

	public override void OnClick ()
	{
		if (automatic) {
			OnHoldClick ();
		} else {
			TryToShoot();
		}
	}
	
	public override void OnHoldClick ()
	{
		TryToShoot ();
	}

	void TryToShoot () {
		if (fireRateTimer <= 0f)
		{
			if (currClipSize > 0)
			{
				fireRateTimer = fireRate;
				Shoot();
				currClipSize--;
			} else
			{
				print("Magazine Empty, Press 'R' to Reload");
			}
		}
	}
	
	public override void OnStopClick ()
	{

	}
	
	void Shoot ()
	{
		RaycastHit hit;
		Physics.Raycast (mainCamera.transform.position, mainCamera.transform.forward, out hit);
		print("Shooting!");
		GameObject shot = (GameObject) Instantiate (bullet, endOfBarrel.transform.position, Quaternion.identity);
		Quaternion direction;
		if (hit.collider != null) {
			direction = Quaternion.FromToRotation(shot.transform.forward, hit.point - shot.transform.position);
		} else {
			direction = Quaternion.FromToRotation(shot.transform.forward, (mainCamera.transform.position + (mainCamera.transform.forward * 100f)) - shot.transform.position);
		}
		shot.transform.rotation = direction;
		shot.GetComponent<Rigidbody> ().velocity = shot.transform.forward * bulletVelocity;
		shot.GetComponent<BulletController> ().damage = this.damage;
	}


	public override void ReloadFunction()
	{
		if (!reloading && currClipSize != clipSize)
		{
			print("Reloading!");
			reloading = true;
			Invoke("Reload", reloadSpeed);
		}
	}
	
	void Reload()
	{
		currClipSize = clipSize;
		print("Reloaded!");
		reloading = false;
	}

}
