using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rebotar : MonoBehaviour {
	public float power=10f;
	Collision auxiliar;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnCollisionEnter (Collision col)
	{
		auxiliar=col;
		Vector3 jugador=auxiliar.transform.position;
		Vector3 contacto=auxiliar.contacts[0].point;
		rebote_local(contacto,jugador);
	}
	
	private void rebote_local(Vector3 contacto, Vector3 jugador)
	{
		//DISTANCIA ENTRE CLICK Y JUGADOR SE NORMALIZA Y SE DA FUERZA OPUESTA
		if(auxiliar.gameObject.GetComponent<Rigidbody>()!=null){
			Rigidbody rb= auxiliar.gameObject.GetComponent<Rigidbody>();
			Vector3 dir = contacto - jugador;
			dir = -dir.normalized;
			rb.AddForce(dir*power); //FUNCIONA ACTUAL
		}
	}

}
