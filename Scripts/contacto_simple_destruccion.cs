using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contacto_simple_destruccion : MonoBehaviour {
	
	private audiocontroller audiocontroller_aux;
	void Start () {
		audiocontroller_aux=FindObjectOfType<audiocontroller>();	
	}
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
  	{
		if(other.gameObject.tag == "Player")
		{
			/* if(activar_sonido)
			{
				audiocontroller_aux.reproducir(sonido);
			} */
			Destroy(gameObject);
		}
  	}	
}
