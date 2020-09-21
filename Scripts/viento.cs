using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viento : MonoBehaviour {
	
	[Range(0f,360f)]
    public float angulo;

	public float fuerza;
	public float fuerza_de_salida;
	
	public float largoGiz;
	private float rad_giz;
	private GameObject objeto_jugador_aux;
	private bool control_aux;
	
	void FixedUpdate()
	{
		if (control_aux && objeto_jugador_aux != null)
			{
				float radianes = angulo * (Mathf.PI / 180);
				rad_giz = radianes;
				objeto_jugador_aux.GetComponent<Rigidbody>().AddForce
				(
					new Vector3
						(
						Mathf.Cos(radianes),
						Mathf.Sin(radianes),
						0
					) * fuerza
				);
			}
		else
		{
			if(objeto_jugador_aux == null)
			{
				control_aux = false;
				objeto_jugador_aux = null;
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && other.gameObject.activeSelf)
		{
			control_aux=true;
			objeto_jugador_aux=other.gameObject;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			control_aux=false;
			if(objeto_jugador_aux.GetComponent<Rigidbody>().velocity.magnitude > 40)
			{
				objeto_jugador_aux.GetComponent<Rigidbody>().velocity = objeto_jugador_aux.GetComponent<Rigidbody>().velocity.normalized * fuerza_de_salida;
			}
		}
	}

	void OnDrawGizmosSelected()
    {
        //Dibuja linea desde origen a destino
		float radianes = angulo * (Mathf.PI / 180);
		Gizmos.color = Color.green;
        Gizmos.DrawLine
		(
			new Vector3(transform.position.x,transform.position.y,0), 
			new Vector3
			(
				(Mathf.Cos(radianes) * largoGiz) + transform.position.x,
				(Mathf.Sin(radianes) * largoGiz) + transform.position.y,
				0
			)
		);
    }

}
