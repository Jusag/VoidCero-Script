using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seguir_con_correccion : MonoBehaviour {
	public Transform objetivo;
	private GameController gamecontroller_aux;
	public float altura;
	public float correcion_x, correcion_y, correcion_z;
	void Start ()
	{
		if(GameObject.Find("GameController"))
		{
			gamecontroller_aux=GameObject.Find("GameController").GetComponent<GameController>();
			objetivo=gamecontroller_aux.jugadoractual().transform;
		}
	}
	void Update () {
		if(objetivo!=null)
		{
			transform.position=new Vector3
			(
				objetivo.position.x+correcion_x, 
				objetivo.position.y+correcion_y, 
				altura+correcion_z
			);
		}
	}
	public void asiganrobjetivo(GameObject obj)
	{
		objetivo=obj.transform;
	}
}
