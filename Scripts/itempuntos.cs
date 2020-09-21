using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itempuntos : MonoBehaviour {
	public int puntos;
	private GameController gamecontroller_aux;
	private audiocontroller audiocontroller_aux;
	
	[Header("Sonido A Reproducir...")]
    public string sonido_reproducir;
	void Start ()
	{
		gamecontroller_aux=FindObjectOfType<GameController>();
		audiocontroller_aux=FindObjectOfType<audiocontroller>();
	}
	
	void OnTriggerEnter(Collider other)
  	{
		if(other.gameObject.tag == "Player")
		{
			gamecontroller_aux.sumarpuntos(puntos);
			if(sonido_reproducir!=null&&audiocontroller_aux!=null)
        	{
            audiocontroller_aux.reproducir(sonido_reproducir);
        	}
			Destroy(gameObject);
		}
		
  	}	

}
