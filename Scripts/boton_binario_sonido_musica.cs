using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boton_binario_sonido_musica : MonoBehaviour {
	private GameController gamecontroller_auxiliar;
	//private audiocontroller audiocontroller_aux;
	private bool estado;
	private Text texto;
	// Use this for initialization
	private int musica_int;
	private int sonido_int;
	void Awake() 
	{
		if(gameObject.name == "Musica_Boton")
		{
			musica_int = PlayerPrefs.GetInt("musica" , 1);
			if(musica_int == 1 )
			{
				estado=true;
			}
			else
			{
				if(musica_int == 0 )
				{
					estado=false;
				}
			}
		}
		if(gameObject.name == "Sonido_Boton")
		{
			sonido_int = PlayerPrefs.GetInt("sonido" , 1);
			if(sonido_int == 1 )
			{
				estado=true;
			}
			else
			{
				if(sonido_int == 0 )
				{
					estado=false;
				}
			}
		}
		
	}
	void Start () {
		//gamecontroller_auxiliar=FindObjectOfType<GameController>();
		gamecontroller_auxiliar=GameObject.Find("GameController").GetComponent<GameController>();
		texto = gameObject.GetComponentInChildren<Text>();
		tocado_inicial();
	}
	public void tocado_inicial()
	{
		//estado=!estado;
		if(estado)
		{
			texto.color=Color.white;
			estado = !estado;
		}
		else
		{
			texto.color=Color.grey;
			estado = !estado;
		}
	}
	public void tocado()
	{
		//estado=!estado;
		if(estado)
		{
			texto.color=Color.white;
			if(gameObject.name == "Musica_Boton")
			{
				gamecontroller_auxiliar.musica_activar(true);
				estado = !estado;
				PlayerPrefs.SetInt("musica" , 1);

			}
			else if(gameObject.name == "Sonido_Boton")
			{
				gamecontroller_auxiliar.sonido_activar(true);
				estado = !estado;
				PlayerPrefs.SetInt("sonido" , 1);
			}
		}
		else
		{
			texto.color=Color.grey;
			if(gameObject.name=="Musica_Boton")
			{
				gamecontroller_auxiliar.musica_activar(false);
				estado = !estado;
				PlayerPrefs.SetInt("musica" , 0);
			}
			else if(gameObject.name=="Sonido_Boton")
			{
				gamecontroller_auxiliar.sonido_activar(false);
				estado = !estado;
				PlayerPrefs.SetInt("sonido" , 0);
			}
		}
	}
	public bool estado_actual()
	{
		return estado;		
	}
}
