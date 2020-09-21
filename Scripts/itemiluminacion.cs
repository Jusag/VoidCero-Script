using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemiluminacion : MonoBehaviour
{
	private GameController gamecontroller_aux;
	private jugador_control_animacion jugadoranim_aux;
	private activarodesactivarcontacto activar_evento;
	public Light luz_jugador;
	//public Light luzlocal;
	public float defecto_angulo;
	public float speed;
	public float min_angulo, max_angulo, min_intensidad,max_intensidad, tiempoampliado,tiemporeducido;
	private float actual_angulo,actual_intensidad,actual_rango;
	public bool agrandar;
	public bool triger,colider;
	public bool fijo;
	
	[Header("Efecto Particula")]
	public GameObject efecto_particula;
	private GameObject aux_efecto; //efecto al agarrarla
	

	[Header("Es este GameObject el enigo BLINDER?")]
	public bool quitarpropiedadenemigo; //Para evitar doble contacto
	
	[Header("Creo Objecto al tener contacto?")]
	public GameObject crearalcontactar; //Objeto de particulas a crear cuando toma contacto con jugador
	private GameObject aux_efecto_pick; //aura


	void Start () {
		//VALORES AL CREAR EL OBJETO
		if(GameObject.Find("GameController")){
			gamecontroller_aux=GameObject.Find("GameController").GetComponent<GameController>();
			luz_jugador=gamecontroller_aux.obt_luz_jugador();
		}
		jugadoranim_aux=FindObjectOfType<jugador_control_animacion>();
		activar_evento=this.GetComponent<activarodesactivarcontacto>();
		if(luz_jugador!=null){
			actual_angulo=80;
			actual_intensidad=170;
			actual_rango=30;
		}else Debug.Log("LUZ Es nulo");
	}
	//LUZ JUGADOR
	//ANGULO POR DEFECTO 80
	//ANGULO MAXIMO 120
	//INTENSIDAD POR DEFECTO 170 
	//RANGO POR DEFECTO 30
	private IEnumerator Ampliarluz()
	{
		//INCREMNTO DE RANGOS E INTENSIDAD DE LUZ DE JUGADOR Y LUZ DE FONDO DE SIGUIENTE NIVEL
		
		while(luz_jugador.spotAngle<max_angulo)
		{
			luz_jugador.spotAngle+=Time.deltaTime*speed;
			speed+=speed/15;
			if(luz_jugador.intensity<max_intensidad)
			{
				luz_jugador.intensity+=Time.deltaTime*speed;
			}
			if(luz_jugador.range<320)
			{
				luz_jugador.range+=Time.deltaTime*(speed*1f);
			}
			yield return null;
		}
		luz_jugador.spotAngle=max_angulo;
		luz_jugador.intensity=max_intensidad;
		luz_jugador.range=320;
		speed=1;
		yield return new WaitForSeconds(tiempoampliado);
		//DECREMENTO DE RANGOS E INTENSIDAD DE LUZ DE JUGADOR Y LUZ DE FONDO DE SIGUIENTE NIVEL
		if(!fijo)
		{
			while(luz_jugador.spotAngle>80)
			{
				//luzjugadr.spotAngle-=Time.deltaTime*speed;
				speed+=speed/30;
				if(luz_jugador.spotAngle>80)
				{
					luz_jugador.spotAngle-=Time.deltaTime*speed;
				}
				if(luz_jugador.intensity>min_intensidad)
				{
					luz_jugador.intensity-=Time.deltaTime*speed;
				}
				if(luz_jugador.range>50)
				{
					luz_jugador.range-=Time.deltaTime*(speed*1.5f);
				}
				yield return null;
			}
			luz_jugador.spotAngle=80;
			luz_jugador.intensity=170;
			luz_jugador.range=30;
			speed=1;
		}
		jugadoranim_aux.desactivarluz();
		if(activar_evento!=null)
		{
			activar_evento.resultado_metodo();
		}
		Destroy(aux_efecto_pick);
		Destroy(aux_efecto);
		Destroy(gameObject);
	}
	private IEnumerator Reducirluz()
	{
		while(luz_jugador.spotAngle>min_angulo)
		{
			luz_jugador.spotAngle-=Time.deltaTime*speed;
			speed+=speed/15;
			yield return null;
		}
		speed=1;
		yield return new WaitForSeconds(tiemporeducido);
		while(luz_jugador.spotAngle<80)
		{
			luz_jugador.spotAngle+=Time.deltaTime*speed;
			speed+=speed/30;
			yield return null;
		}
		luz_jugador.spotAngle=actual_angulo;
		speed=1;
		Destroy(gameObject);
	}
	void OnTriggerEnter(Collider other)
  	{
		if(triger){
			if(other.gameObject.tag == "Player")
			{
				if(agrandar)
				{

					jugadoranim_aux.activarluz();
					StartCoroutine(Ampliarluz());
					if(crearalcontactar!=null)
					{
						aux_efecto_pick = Instantiate(crearalcontactar, transform.position, Quaternion.identity);
					}
					if(efecto_particula != null)
					{
						aux_efecto = Instantiate(efecto_particula, gamecontroller_aux.jugadoractual().transform.position, Quaternion.identity);
					}
				}
				else
				{
					StartCoroutine(Reducirluz());
				}
				gameObject.transform.localScale=new Vector3(0, 0, 0);
				gameObject.transform.position=new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,20f);
				Collider aux=GetComponent<Collider>();
				aux.isTrigger = true;
			}
		}
  	}
	void OnCollisionEnter(Collision other)
  	{
		if(colider){
			if(other.gameObject.tag == "Player")
			{
				if(quitarpropiedadenemigo)
				{
					PathFollower path_aux=GetComponent<PathFollower>();
					path_aux.desactivar_fun_enemigo();
				}
				
				if(agrandar)
				{
					jugadoranim_aux.activarluz();
					StartCoroutine(Ampliarluz());
				}
				else
				{
					GameObject aux_obj=Instantiate(crearalcontactar, other.transform.position, Quaternion.identity);
					aux_obj.GetComponent<seguir>().asiganrobjetivo(other.gameObject);
					StartCoroutine(Reducirluz());
				}
				gameObject.transform.localScale=new Vector3(0, 0, 0);
				gameObject.transform.position=new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,20f);
				Collider aux=GetComponent<Collider>();
				aux.isTrigger = true;
			}
		}
  	}
	public void mod_speed(float valor)
	{
		speed=valor;
	}
}
