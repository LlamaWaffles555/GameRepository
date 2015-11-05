using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public static MainMenuController menu;

	[HideInInspector] public bool isVisible;

	// Use this for initialization
	void Start () {
		menu = this;
		isVisible = false;
		UpdateVisibility ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateVisibility () {
		this.gameObject.SetActive (isVisible);
	}
}
