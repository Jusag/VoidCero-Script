using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seguir : MonoBehaviour {
	public Transform objetivo;
	private GameController gamecontroller_aux;
	public float altura;
	void Start () {
		if(GameObject.Find("GameController"))
		{
			gamecontroller_aux=GameObject.Find("GameController").GetComponent<GameController>();
			objetivo=gamecontroller_aux.jugadoractual().transform;
		}
		
	}
	void Update () {
		if(objetivo!=null)
		{
			transform.position=new Vector3(objetivo.position.x, objetivo.position.y, altura);
		}
		else
		{
			if(gameObject.tag=="Luces")
			{
				gameObject.SetActive(false);
			}
		}
	}
	public void asiganrobjetivo(GameObject obj)
	{
		objetivo=obj.transform;
	}

	public Vector3 seguir_cord()
	{
		return new Vector3(objetivo.position.x, objetivo.position.y, altura);
	}
}
