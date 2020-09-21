using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activarodesactivarcontacto : MonoBehaviour 
{
	public GameObject objetivo;
	public GameObject resultado;
	private bool usado;
	
	public void resultado_metodo()
	{
		resultado.SetActive(true);
	}
}
