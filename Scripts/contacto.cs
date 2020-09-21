using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class contacto : MonoBehaviour {
	private GameController gamecontroller_aux;
	//VARIABLES A METODO REBOTE

	private Collision auxiliar;
	private bool estado;

	//public bool luz;

	[Header("Puntos por CHOCAR")]
	public int puntos;
	//public bool usartrigger;

	[Header("Propiedades - Si es enemigo o pared")]
	public bool matar;
	public bool rebotar;
	public float poderderebote=1f;
	//public bool desapareceralcontacto; //Para usar con enemigos

	[Header("Propiedades - Si es item")]
	public bool activaralcontacto; //Para usar con obejtos ENEMIGOS u EVENTOS

	[Header("Creo Objecto al tener contacto?")]
	public bool creoalcontactar;
	public GameObject acrear;

	[Header("A crear para muerte de JUGADOR SOLAMENTE")]
	public GameObject acrearparamuerte;

	void Start () {
		gamecontroller_aux=FindObjectOfType<GameController>();
	}
	
	void Update () {
	}
	//void OnCollisionEnter(Collision other)
	//void OnTriggerEnter(Collider other)
	void OnCollisionEnter (Collision other)
    {
		switch(other.gameObject.tag)
		{
			case "Enemy":
				{

				}
			break;
			case "Player":
			{
				if(matar)
				{
					//Freno a CERO el movimiento del jugador
					other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
					
					//Se activan comando al perder ( perdida de vidas bloquear navegacion)
					gamecontroller_aux.perder();
					
					//Se controlan puntajes maximos al perder
					gamecontroller_aux.maximo_puntaje_control();
					
					//Se crea efecto de muerte
					Instantiate(acrearparamuerte, other.transform.position, Quaternion.identity);
					
					//Se apaga jugador
					other.gameObject.SetActive(false);
					

					if(other.gameObject.GetComponent<jugador_control_animacion>()!=null){
					jugador_control_animacion aux=other.gameObject.GetComponent<jugador_control_animacion>();
					
					if(aux.estado_luz())
					{
						aux.desactivarluz();
					}
					gamecontroller_aux.resetear_propiedades_luz_jugador();
				}
				estado=false;
				}
				else
				{
					if(rebotar)
					{
						auxiliar=other;
						Vector3 jugador=auxiliar.transform.position;
						Vector3 contacto=auxiliar.contacts[0].point;
						rebote_local(contacto,jugador);
					}
					else
					{
						if(activaralcontacto)
						{
							if(this.GetComponent<activarodesactivarcontacto>()!=null)
							{
							activarodesactivarcontacto aux=this.GetComponent<activarodesactivarcontacto>();
							aux.resultado_metodo();
							}
						}
					}
				}
			}
			break;
			case "Pared_tag":
			{
				if(gameObject.tag == "Enemy")
				{
					//Creo efecto particulas de "destruccion"
					if(creoalcontactar)
					{
						Instantiate(acrear,transform.position,Quaternion.identity);					
					}
					gamecontroller_aux.sumarpuntos(puntos); //Sumo Puntos
					//Detengo sonidos y efecto especiales de persecucion
					if(GetComponent<PathFollower>() != null)
					{
						PathFollower path_aux = GetComponent<PathFollower>();
						path_aux.detener_sonidos_persecucion_efectos();
					}
					StartCoroutine(retraso());
				}
			}
			break;
		}
    }
	
	private void rebote_local(Vector3 contacto, Vector3 jugador)
	{
		//Calculo para rebotar
		Vector3 dir = contacto - jugador;
        dir = -dir.normalized;
		Rigidbody rb= auxiliar.gameObject.GetComponent<Rigidbody>();
		rb.AddForce(dir*poderderebote);
	}
	public void animacionseguir(bool aux)
	{
		estado=aux;
	}
	public bool estadoactual()
	{
		return estado;
	}
	private IEnumerator retraso() //Retraso entre desaparicion de enemigo y activacion efecto especial post muerte
	{
		yield return new WaitForSeconds(0.09f);
		Destroy(gameObject);
	}
}
