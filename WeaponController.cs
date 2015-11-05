using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	protected PlayerController player;
	protected GameObject mainCamera;
	public GameObject endOfBarrel;

	public void setPlayerController(PlayerController playerController)
	{
		player = playerController;
		mainCamera = player.mainCamera;
	}

	public virtual void OnClick ()
	{

    }

	public virtual void OnHoldClick ()
	{

	}

	public virtual void OnStopClick ()
	{

	}

	public virtual void AltFire()
	{

	}

	public virtual void ReloadFunction()
	{

	}

}
