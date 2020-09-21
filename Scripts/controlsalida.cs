using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlsalida : MonoBehaviour {
	public GameObject salida_obj;
	public int puntosparapasar;
	private int puntostraidosaliniciar;
	private bool control;
	private GameController game_aux;
	void Start () {
		game_aux=FindObjectOfType<GameController>();
		control=false;
		puntostraidosaliniciar=game_aux.puntosacuales();
	}
	
	void Update () {
		if(!control&&(game_aux.puntosacuales()-puntostraidosaliniciar>=puntosparapasar))
		{
			salida_obj.SetActive(true);
			control=true;
		}
	}
}
