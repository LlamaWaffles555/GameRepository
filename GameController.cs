using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public static List<WeaponController> weapons;
	
	void Start () {
		WeaponsInit ();
	}

	void WeaponsInit ()
	{
		weapons = new List<WeaponController> ()
		{
			//----------------ADD_GUNS_HERE-------------------//
			new ConventionalWeapon(),
			new ConventionalWeapon(false, 35f, 0.9f, 4, 1.9f)
		};
	}
	
	void Update () {
	
	}
}
