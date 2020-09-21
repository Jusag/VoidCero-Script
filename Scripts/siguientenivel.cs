using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siguientenivel : MonoBehaviour {
	private GameController gamecontroller_aux;
	private audiocontroller audiocontroller_aux;
	public GameObject crear_salida;

	private Collider collider_aux;
	
	public bool apagaralsalir;
	
	void Start () {
		gamecontroller_aux=FindObjectOfType<GameController>();
		audiocontroller_aux=FindObjectOfType<audiocontroller>();
	}
	void Update () {
	
	}
	//void OnCollisionEnter(Collision other)
	void OnTriggerEnter(Collider other)
  	{	
		collider_aux=other;
		if(other.gameObject.tag=="Player")
		{
			gamecontroller_aux.maximo_puntaje_control(); //Control Puntaje maximo
			
			//Sonido Encontrado Salida
			audiocontroller_aux.reproducir("Avanzar_Nivel");
			
			//Apago Jugador y dejo velocidad en cero
			other.gameObject.SetActive(false);
			other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			
			//Desactivo controles y cargo siguiente nivel
			gamecontroller_aux.controlenevento_desactivar();
			gamecontroller_aux.siguiente_nivel();
			
			if(apagaralsalir){
				gameObject.SetActive(false);
				if(crear_salida!=null)
				{
					Instantiate(crear_salida,transform.position,Quaternion.identity);
				}
			}
		}
	}
}
