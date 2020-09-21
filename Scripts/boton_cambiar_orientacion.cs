using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boton_cambiar_orientacion : MonoBehaviour {
	//definir primera config cuando arranca por primera vez
	private GameController gamecontroller_auxiliar;
	public GameObject atras;
	public GameObject adelante;
	private int sentido_numerico;

	// Use this for initialization
	void Start () {
		//gamecontroller_auxiliar=FindObjectOfType<GameController>();
		gamecontroller_auxiliar=GameObject.Find("GameController").GetComponent<GameController>();
		sentido_numerico = PlayerPrefs.GetInt("movimiento", 1);
		cambiarinicial();
	}

	// Update is called once per frame
	public void cambiarinicial()
	{
		if (sentido_numerico == 1)
		{
			atras.SetActive(false);
			adelante.SetActive(true);
			sentido_numerico = 0;
		}
		else
		{
			if (sentido_numerico == 0)
			{
				adelante.SetActive(false);
				atras.SetActive(true);
				sentido_numerico = 1;
			}
		}
	}
	public void cambiar()
	{
		//if(atras.activeSelf&&!adelante.activeSelf)
		if (sentido_numerico == 1)
		{
			atras.SetActive(false);
			adelante.SetActive(true);
			gamecontroller_auxiliar.desplazamiento(true);
			PlayerPrefs.SetInt("movimiento", 1);
			sentido_numerico = 0;
		}
		else
		{
			if (sentido_numerico == 0)
			{
				adelante.SetActive(false);
				atras.SetActive(true);
				gamecontroller_auxiliar.desplazamiento(false);
				PlayerPrefs.SetInt("movimiento", 0);
				sentido_numerico = 1;
			}
		}
	}

}
