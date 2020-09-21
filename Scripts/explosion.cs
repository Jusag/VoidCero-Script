using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {
	
	public float power=10f;
	public Camera cam; //CAMARA
	private Vector3 clickPos; //POSICION DEL CLICK
	public GameObject prefabtouch; //PLANO TOUCH
	private GameController gamecontroller_aux;
	private int desplaza;

	//prueba screentoworld
	Vector3 pos;

	void Start () {
		cam=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
		pos=new Vector3(0f,0f,0f);
		gamecontroller_aux=GameObject.FindObjectOfType<GameController>();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)&&gamecontroller_aux.controles()&&gameObject.activeSelf)
		{
			//RAYCAST BUSCA PUNTO DE COLISION CON plano en origen
			Ray ray=cam.ScreenPointToRay(Input.mousePosition);
			Plane plane=new Plane(Vector3.forward, transform.position);
			float dist=0;
			if(plane.Raycast(ray, out dist))
			{
				pos=ray.GetPoint(dist);
			}
			//Debug.Log(pos); informa la posicion de cursor en el mundo
			GameObject aux=Instantiate(prefabtouch,pos,Quaternion.identity);
			detonacion(pos);
		}
	}
	private void detonacion(Vector3 aux)
	{
		if(gamecontroller_aux.desplazamiento_estado())
		{
			desplaza=1;
		}
		else
		{
			desplaza=-1;
		}
		//DISTANCIA ENTRE CLICK Y JUGADOR SE NORMALIZA Y SE DA FUERZA OPUESTA
		Rigidbody rb= GetComponent<Rigidbody>();
		
		
		Vector3 dir = aux - transform.position;
        dir = (dir.normalized)*desplaza;
		rb.AddForce(dir*power);
		
		if(gamecontroller_aux.tutorial_estado())
		{
			if(rb.velocity.magnitude>5f)
			{
				rb.velocity=rb.velocity.normalized*4.8f;
			}
		}
		else
		{
			if(rb.velocity.magnitude>40f)
			{
				rb.velocity=rb.velocity.normalized*39.5f;
			}
		}
		//Vector3 auxlog=rb.velocity;
		//Debug.Log(rb.velocity.magnitude);
		//Debug.Log(rb.velocity);
	}
}
