using UnityEngine;
using System.Collections;

public class ETW : WeaponController {

	protected LineRenderer beam;
	
	protected float damage; // Damage per second
	protected float maxHeat;
	protected float heatProduction; // Heat produced per shot/second
	protected float maxEnergy;
	protected float energyConsumption; // Energy consumed per shot/second
	protected float coolDelay; // delay between shooting and cooling
	protected float coolRate; // Heat removed per second when cooling
	protected float activeCoolRate; // modifier for how much faster you actively cool your weapon
	
	protected float coolDelayTimer;
	protected bool cooling;
	protected bool shooting;
	protected float currHeat;
	protected float currEnergy;

	void Awake () {
		beam = new LineRenderer ();
		beam.transform.parent = this.gameObject.transform.parent;
		beam.enabled = false;
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.R)) {
			HoldReload();
		}
	}

	public virtual void HoldReload () {

	}

}
