using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuerzazero : MonoBehaviour {
	public bool zero;
	public bool mantener_controles;
	private GameController gamecontroller_aux;
	
	public bool usarobjeto;
	public GameObject objetoausar;
	
	public bool desaparecerobjeto;
	public GameObject objetoadesaparecer;

	void Start () {
		gamecontroller_aux=FindObjectOfType<GameController>();
	}
	void Update () {
	}

	void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag=="Player")
		{	
			if(zero)
			{
				other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
			
			if(!mantener_controles)
			{
				gamecontroller_aux.controlenevento_activar();
				gamecontroller_aux.control_desactivar();

			}
			
			///HACER APARECER OBJETO
			if(usarobjeto)
			{
				if(objetoausar != null){
					objetoausar.SetActive(true);
				}
			}
			else
			{
				/* if(!usarobjeto)
				{
					if(objetoausar!=null){
						objetoausar.SetActive(false);
					}
				} */
			}
			///HACER DESAPARECER OBJETO
			if(desaparecerobjeto)
			{
				if(objetoadesaparecer!=null)
				{
					objetoadesaparecer.SetActive(false);
				}
			}
			else
			{
				/* if(!desaparecerobjeto)
				{
					if(objetoadesaparecer!=null)
					{
						objetoadesaparecer.SetActive(false);
					}
				} */
			}
		}
	}
}
