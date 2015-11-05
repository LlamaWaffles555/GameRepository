using UnityEngine;
using System.Collections;

public class ETW_Continuous : ETW {

	void Start () {
		currHeat = 0f;
		currEnergy = maxEnergy;
		cooling = false;
		shooting = false;
		coolDelayTimer = coolDelay;
	}
	
	void Update () {
		if (!shooting && coolDelayTimer > 0f) {
			coolDelayTimer = Mathf.Clamp(coolDelayTimer - Time.deltaTime, 0f, coolDelay);
		} else if (!shooting && coolDelayTimer <= 0f) {
			cooling = true;
			currHeat = Mathf.Clamp(currHeat - coolRate * Time.deltaTime, 0f, maxHeat);
		}
	}

	public ETW_Continuous () : this (20f, 100f, 13f, 100f, 1f, 2f, 10f, 2f) {}

	public ETW_Continuous (float damage, float maxHeat, float heatProduction, float maxEnergy, float energyConsumption, float coolDelay, float coolRate, float activeCoolRate) {
		this.damage = damage;
		this.maxHeat = maxHeat;
		this.heatProduction = heatProduction;
		this.maxEnergy = maxEnergy;
		this.energyConsumption = energyConsumption;
		this.coolDelay = coolDelay;
		this.coolRate = coolRate;
		this.activeCoolRate = activeCoolRate;
	}

	public override void OnClick ()
	{
		shooting = true;
		cooling = false;
	}
	
	public override void OnHoldClick ()
	{
		if (currHeat < maxHeat && currEnergy > 0f) {
			Shoot ();
			currHeat = Mathf.Clamp (currHeat + heatProduction * Time.deltaTime, 0f, maxHeat);
			currEnergy = Mathf.Clamp (currEnergy - energyConsumption * Time.deltaTime, 0f, maxEnergy);
		}
	}

	void Shoot() {
		RaycastHit hit;
		Physics.Raycast (mainCamera.transform.position, mainCamera.transform.forward, out hit);
		Vector3 point;
		if (hit.collider == null) {
			point = mainCamera.transform.position + (mainCamera.transform.forward * 150f);
		} else {
			point = hit.point;
		}
		beam.enabled = true;
		beam.SetVertexCount(2);
		beam.SetWidth (0.35f, 0.35f);
		beam.SetColors (Color.green, Color.green);
		beam.SetPosition (0, endOfBarrel.transform.position);
		beam.SetPosition (1, point);
		RaycastHit hitInfo;
		Physics.Raycast (endOfBarrel.transform.position, point, out hitInfo);
		if (hit.collider != null) {
			if (hit.collider.gameObject.CompareTag ("Enemy")) {
				print ("Hit an Enemy!");
				hit.collider.gameObject.GetComponent<EnemyController> ().TakeDamage (damage);
			} else if (hit.collider.gameObject.CompareTag ("Player")) {
				print ("Hit a Player!");
				hit.collider.gameObject.GetComponent<PlayerController> ().TakeDamage (damage);
			} else {
				print ("Hit something not an Enemy/Player");
			}
		} else {
			print ("Hit nothing");
		}
	}
	
	public override void OnStopClick ()
	{
		shooting = false;
	}

	public override void HoldReload ()
	{
		if (cooling) {
			currHeat = Mathf.Clamp(currHeat - coolRate * activeCoolRate * Time.deltaTime, 0f, maxHeat);
		}
	}

}
