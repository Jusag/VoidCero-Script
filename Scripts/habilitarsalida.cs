using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class habilitarsalida : MonoBehaviour {
	private GameController gamecontroler_local;
	public GameObject salidabarrera;
	void Start () {
		gamecontroler_local=FindObjectOfType<GameController>();
	}
	void Update () {
		if(gamecontroler_local.puntosacuales()>250)
		{
			salidabarrera.SetActive(false);
		}
	}
}
