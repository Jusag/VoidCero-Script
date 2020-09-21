using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boton_continuar : MonoBehaviour
{

	private GameController gamecontroller_aux;
	private buscadorads busca_local;
	void Start()
	{
		gamecontroller_aux = GameObject.Find("GameController").GetComponent<GameController>();
		busca_local = FindObjectOfType<buscadorads>();
	}
	public void perdervidaycontinuar() //3 Vidas en total, 3 intentos
	{
		gamecontroller_aux.continue_game();
		//StartCoroutine(publicidad());
		/* if(busca_local.estado_de_ads())
		{
			busca_local.botoneta_ads();
			gamecontroller_aux.continue_game();
		} */
	}
	/* private IEnumerator publicidad()
	{
		yield return new WaitForSeconds(3);
	} */
}