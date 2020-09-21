using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonrestart : MonoBehaviour {

	private GameController gamecontroller_aux;
	void Start () {
		gamecontroller_aux=FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void reinicio()
	{
		gamecontroller_aux.restart();
	}
}
